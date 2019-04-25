using System.Collections.Generic;
using Lucene.Net;

namespace Lucene.Search
{
    public interface ISearchEngine
    {
         void BuildIndex(IEnumerable<BookForLucene> books);
         SearchResults Search(string query);

    }
}