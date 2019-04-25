using System.Collections.Generic;

namespace Lucene.Search
{
    public class SearchResults
    {
        public SearchResults() => Hits = new List<Hit>();
        public string Time { get; set; }
        public int TotalHits { get; set; }
        public IList<Hit> Hits { get; set; }
    }

    public class Hit {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Snippet { get; set; }
        public float Score { get; set; }
    }
}