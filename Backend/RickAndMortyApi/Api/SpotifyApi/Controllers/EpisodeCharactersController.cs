using Contract.Request.Characters;
using Contract.Response.EpisodeCharacters;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rickandmorty/episodes/{episodeId}")]
    public class EpisodeCharactersController : ControllerBase
    {
        private readonly EpisodeCharactersOperations episodeCharactersOperations;
        public EpisodeCharactersController(EpisodeCharactersOperations episodeCharactersOperations)
        {
            this.episodeCharactersOperations = episodeCharactersOperations;
        }

        [HttpGet]
        [Route("characters")]
        public ActionResult<GetEpisodeCharactersResponse> GetEpisodeCharacters([FromRoute] int episodeId, [FromQuery] SearchCharactersRequest request)
        {
            var characters = episodeCharactersOperations.GetEpisodeCharacters(episodeId, request.pageNumber, request.pageCount);
            GetEpisodeCharactersResponse response = new GetEpisodeCharactersResponse();

            foreach (var character in characters)
            {
                response.episodeCharacters.Add(new GetEpisodeCharactersResponse.EpisodeCharacters()
                {
                    id = character.Id,
                    name = character.Name,
                    status = (int)character.Status,
                    gender = (int)character.Gender,
                    species = (int)character.Species,
                    locationId = character.LocationId,
                    originId = character.OriginId,

                });
            }

            return new JsonResult(response);
        }
    }
}
