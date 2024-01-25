using Contract.Request.Characters;
using Contract.Request.Episodes;
using Contract.Response.Characters;
using Contract.Response.Episodes;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rickandmorty/episodes")]
    public class EpisodesController : ControllerBase
    {
        private readonly EpisodeOperations episodeOperations;
        public EpisodesController(EpisodeOperations episodeOperations)
        {
            this.episodeOperations = episodeOperations;
        }

        [HttpGet]
        public ActionResult<SearchEpisodesResponse> Search([FromQuery] SearchEpisodesRequest request)
        {
            var episodes = episodeOperations.Search(request.name);
            SearchEpisodesResponse response = new SearchEpisodesResponse();
            foreach (var episode in episodes)
            {
                response.episodes.Add(new SearchEpisodesResponse.Episodes()
                {
                    id= episode.Id,
                    name = episode.Name,
                    airDate = episode.AirDate,
                    createdOn = episode.CreatedOn,
                    episodeNo = episode.EpisodeNo,
                });
            }

            return new JsonResult(response);
        }
    }
}
