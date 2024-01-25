using Contract.Request.Characters;
using Contract.Response.Characters;
using DatabaseModel;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rickandmorty/data")]
    public class DataController : ControllerBase
    {
        private readonly MainDbContext mainDbContext;

        public DataController(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        [HttpGet]
        [Route("rickandmorty/episodes")]
        public void InsertEpisodes()
        {
            var data = System.IO.File.ReadAllText("C:\\Users\\onure\\OneDrive\\Masaüstü\\api-data\\episodes.json");
            var episodeData = Newtonsoft.Json.JsonConvert.DeserializeObject<EpisodeData>(data);

            foreach (var episode in episodeData.results)
            {
                var _episode = new DatabaseModel.Entities.Episode()
                {
                    EpisodeNo = episode.episode,
                    Name = episode.name,
                    // AirDate = episode.air_date
                };

                foreach (var character in episode.characters)
                {
                    _episode.Characters.Add(new DatabaseModel.Entities.Character()
                    {
                        Id = Convert.ToInt32(character.Split('/').Last())
                    });
                }

                mainDbContext.Episodes.Add(_episode);
                mainDbContext.SaveChanges();
            }
        }

        //[HttpGet]
        //[Route("rickandmorty/characters")]
        //public void InsertCharacters()
        //{
        //    var data = System.IO.File.ReadAllText("C:\\Users\\onure\\OneDrive\\Masaüstü\\api-data\\characters.json");
        //    var episodeData = Newtonsoft.Json.JsonConvert.DeserializeObject<EpisodeData>(data);

        //    foreach (var episode in episodeData.results)
        //    {
        //        var _episode = new DatabaseModel.Entities.Episode()
        //        {
        //            EpisodeNo = episode.episode,
        //            Name = episode.name,
        //            // AirDate = episode.air_date
        //        };

        //        foreach (var character in episode.characters)
        //        {
        //            _episode.Characters.Add(new DatabaseModel.Entities.Character()
        //            {
        //                Id = Convert.ToInt32(character.Split('/').Last())
        //            });
        //        }

        //        mainDbContext.Episodes.Add(_episode);
        //        mainDbContext.SaveChanges();
        //    }
        //}
    }

    public class CharacterData
    {
        public class Info { }
        public class Result
        {
            public string name { get; set; }
            public string status { get; set; }
            public string species { get; set; }
            public string type { get; set; }
            public string gender { get; set; }
        }

        public CharacterData()
        {
            results = new List<Result>();
        }

        public List<Result> results { get; set; }
    }

    public class EpisodeData
    {
        public class Info { }
        public class Result
        {
            public string name { get; set; }
            public string episode { get; set; }
            public string air_date { get; set; }
            public List<string> characters { get; set; }
        }

        public EpisodeData()
        {
            results = new List<Result>();
        }

        public List<Result> results { get; set; }
    }
}
