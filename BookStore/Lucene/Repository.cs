using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Lucene;

namespace Lucene
{
    public class Repository
    {
        public static IEnumerable<BookForLucene> GetMoviesFromFile() => JsonConvert.DeserializeObject<List<BookForLucene>>(File.ReadAllText(Settings.MovieJsonFile));
    }
}