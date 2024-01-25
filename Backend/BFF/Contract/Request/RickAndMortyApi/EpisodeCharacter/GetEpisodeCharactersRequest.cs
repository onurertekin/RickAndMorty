using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.RickAndMortyApi.EpisodeCharacter
{
    public class GetEpisodeCharactersRequest
    {

        public int pageNumber { get; set; }
        public int pageCount { get; set; }
    }
}
