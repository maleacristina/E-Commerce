using System.Collections.Generic;
using Lucene;

namespace Lucene
{
    public class SearchEngine : ISearchEngine
    {
        private readonly BookIndex bookIndex;

        public SearchEngine(IEnumerable<BookForLucene> books)
        {
            bookIndex = new BookIndex(Settings.IndexLocation);
            bookIndex.BuildIndex(books);
        }
        public void BuildIndex(IEnumerable<BookForLucene> books) => bookIndex.BuildIndex(books);

        public SearchResults Search(string query) => bookIndex.Search(query);
    }
}