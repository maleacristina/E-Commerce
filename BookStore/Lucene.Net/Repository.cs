using System.Collections.Generic;
using System.IO;
using Lucene.Net;
using Newtonsoft.Json;

namespace Lucene.Search
{
    public class Repository
    {
        public static IEnumerable<BookForLucene> GetMoviesFromFile() => JsonConvert.DeserializeObject<List<BookForLucene>>(File.ReadAllText(Settings.MovieJsonFile));
    }
}