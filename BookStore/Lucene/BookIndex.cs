using System;
using System.Collections.Generic;
using Lucene;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;


namespace Lucene
{
    internal class BookIndex: IDisposable
    {
        private const LuceneVersion MATCH_LUCENE_VERSION = LuceneVersion.LUCENE_48;
        private const int SNIPPET_LENGTH = 100;
        private readonly IndexWriter writer;
        private readonly Analyzer analyzer;
        private readonly QueryParser queryParser;
        private readonly SearcherManager searchManager;

        public BookIndex(string indexPath)
        {
            analyzer = SetupAnalyzer();
            queryParser = SetupQueryParser(analyzer);
            writer = new IndexWriter(FSDirectory.Open(indexPath), new IndexWriterConfig(MATCH_LUCENE_VERSION, analyzer));
            searchManager = new SearcherManager(writer, true, null);
        }

        private Analyzer SetupAnalyzer() => new StandardAnalyzer(MATCH_LUCENE_VERSION, StandardAnalyzer.STOP_WORDS_SET);
 
        private QueryParser SetupQueryParser(Analyzer analyzer)
        {
            return new MultiFieldQueryParser(
                MATCH_LUCENE_VERSION,
                new[] { "title", "description" },
                analyzer
            );
        }

        public void BuildIndex(IEnumerable<BookForLucene> books)
        {
            if (books == null) throw new ArgumentNullException();

            foreach (var book in books)            
            {
                Document movieDocument = BuildDocument(book);
                writer.UpdateDocument(new Term("id", book.BookId.ToString()), movieDocument);
            }                

            writer.Flush(true, true);
            writer.Commit();
        }

        private Document BuildDocument(BookForLucene book)
        {
            Document doc = new Document
            {
                new StoredField("bookid", book.BookId),
                new TextField("name", book.Name, Field.Store.YES),
                new TextField("description", book.Description, Field.Store.NO),
                new StoredField("snippet", MakeSnippet(book.Description))
            };

            return doc;
        }

        private string MakeSnippet(string description)
        {
            return (string.IsNullOrWhiteSpace(description) || description.Length <= SNIPPET_LENGTH)
                    ? description 
                    : $"{description.Substring(0, SNIPPET_LENGTH)}...";
        }

        public SearchResults Search(string queryString)
        {
            int resultsPerPage = 10;
            Query query = BuildQuery(queryString);
            searchManager.MaybeRefreshBlocking();
            IndexSearcher searcher = searchManager.Acquire();

            try
            {
                TopDocs topdDocs = searcher.Search(query, resultsPerPage);
                return CompileResults(searcher, topdDocs);
            }
            finally
            {
                searchManager.Release(searcher);
                searcher = null;
            }
        }

        private SearchResults CompileResults(IndexSearcher searcher, TopDocs topdDocs)
        {
            SearchResults searchResults = new SearchResults() { TotalHits = topdDocs.TotalHits };
            foreach (var result in topdDocs.ScoreDocs)
            {
                Document document = searcher.Doc(result.Doc);
                int bookId = 0;
                Int32.TryParse(document.GetField("bookid")?.GetStringValue(), out bookId);
                Hit searchResult = new Hit
                {
                    BookId = bookId,
                    Score = result.Score,
                    Description = document.GetField("description")?.GetStringValue(),
                    Name = document.GetField("name")?.GetStringValue(),
                    Snippet = document.GetField("snippet")?.GetStringValue()
                };

                searchResults.Hits.Add(searchResult);
            }

            return searchResults;
        }

        private Query BuildQuery(string queryString) =>  queryParser.Parse(queryString);

        public void Dispose()
        {
            searchManager?.Dispose();
            analyzer?.Dispose();
            writer?.Dispose();
}
    }
}