using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using Lucene.Search;

namespace BookStore.Repository
{
    interface ILuceneRepository
    {
        void BuildIndex(IEnumerable<Book> books);
        SearchResults Search(string query);
    }
}
