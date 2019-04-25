using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BookStore.Models;
using Lucene.Search;

namespace BookStore.Repository
{
    public class LuceneRepository: ILuceneRepository
    {
        private ApplicationDbContext _context;
        private static ISearchEngine _searchEngine;
        private static bool firstTime = true;

        public LuceneRepository()
        {
                _context = new ApplicationDbContext();
                if (_searchEngine == null)
                {
                    DeleteIndexFiles();
                    var books = _context.Books.ToList();
                    IList<BookForLucene> booksForLucene = new List<BookForLucene>();
                    foreach (var book in books)
                    {
                        BookForLucene bookLucene = new BookForLucene
                        {
                            BookId = book.BookId,
                            Name = book.Name,
                            Description = book.Description
                        };
                        booksForLucene.Add(bookLucene);
                    }

                    _searchEngine = new SearchEngine(booksForLucene);
                    firstTime = false;
                }    

        }

        public void DeleteIndexFiles()
        {
            foreach (FileInfo f in new DirectoryInfo(Settings.IndexLocation).GetFiles())
                f.Delete();
        }


        public void BuildIndex(IEnumerable<Book> books)
        {
            IList<BookForLucene> booksForLucene = new List<BookForLucene>();
            foreach (var book in books)
            {
                BookForLucene bookLucene = new BookForLucene
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Description = book.Description
                };
                booksForLucene.Add(bookLucene);
            }
            _searchEngine.BuildIndex(booksForLucene);
        }

        public SearchResults Search(string query)
        {
            return _searchEngine.Search(query);
        }
    }
}