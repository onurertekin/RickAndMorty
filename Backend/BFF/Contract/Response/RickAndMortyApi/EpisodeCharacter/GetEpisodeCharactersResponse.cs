using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.RickAndMortyApi.EpisodeCharacter
{
    public class GetEpisodeCharactersResponse
    {
        public class EpisodeCharacters
        {
            public int id { get; set; }
            public string name { get; set; }
            public int status { get; set; }
            public int gender { get; set; }
            public int species { get; set; }
            public int locationId { get; set; }
            public int originId { get; set; }
        }
        public GetEpisodeCharactersResponse()
        {
            episodeCharacters = new List<EpisodeCharacters>();
        }
        public List<EpisodeCharacters> episodeCharacters { get; set; }
    }
}
