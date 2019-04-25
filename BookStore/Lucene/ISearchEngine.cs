using System.Collections.Generic;
using Lucene;

namespace Lucene
{
    public interface ISearchEngine
    {
         void BuildIndex(IEnumerable<BookForLucene> books);
         SearchResults Search(string query);
    }
}