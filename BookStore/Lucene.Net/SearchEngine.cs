using System.Collections.Generic;
using System.IO;
using Lucene.Net;

namespace Lucene.Search
{
    public class SearchEngine : ISearchEngine
    {
        private readonly BookIndex bookIndex;

        public SearchEngine(IEnumerable<BookForLucene> books)
        {
            bookIndex = new BookIndex(Settings.IndexLocation);
            bookIndex.BuildIndex(books);
        }

        public SearchEngine()
        {
            bookIndex = new BookIndex(Settings.IndexLocation);
        }
        public void BuildIndex(IEnumerable<BookForLucene> books) => bookIndex.BuildIndex(books);

        public SearchResults Search(string query) => bookIndex.Search(query);

        
    }
}