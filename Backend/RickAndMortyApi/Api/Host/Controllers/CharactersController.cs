using Contract.Request.Characters;
using Contract.Response.Characters;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rickandmorty/characters")]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterOperations characterOperations;

        public CharactersController(CharacterOperations characterOperations)
        {
            this.characterOperations = characterOperations;
        }

        [HttpGet]
        public ActionResult<SearchCharactersResponse> Search([FromQuery] SearchCharactersRequest request)
        {
            var characters = characterOperations.Search(request.name, request.status, request.gender, request.species, request.locationId, request.pageNumber, request.pageCount);

            SearchCharactersResponse response = new SearchCharactersResponse();
            foreach (var character in characters)
            {
                response.characters.Add(new SearchCharactersResponse.Characters()
                {
                    id = character.Id,
                    name = character.Name,
                    gender = (int)character.Gender,
                    status = (int)character.Status,
                    species = (int)character.Species,
                    locationId = (int)character.LocationId,
                    originId = (int)character.OriginId,

                });
            }

            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GetSingleCharacterResponse> GetSingle(int id)
        {
            var character = characterOperations.GetSingle(id);
            GetSingleCharacterResponse response = new GetSingleCharacterResponse();
            response.species = (int)character.Species;
            response.status = (int)character.Status;
            response.originId = (int)character.OriginId;
            response.locationId = (int)character.LocationId;
            response.gender = (int)character.Gender;
            response.name = character.Name;

            return new JsonResult(response);

        }
    }
}
