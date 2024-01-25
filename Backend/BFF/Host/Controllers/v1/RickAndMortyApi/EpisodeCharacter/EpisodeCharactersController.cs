using Contract.Request.RickAndMortyApi.EpisodeCharacter;
using Contract.Request.RickAndMortyApi.Location;
using Contract.Response.RickAndMortyApi.Character;
using Contract.Response.RickAndMortyApi.EpisodeCharacter;
using Contract.Response.RickAndMortyApi.Location;
using Host.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Controllers.v1.RickAndMortyApi.EpisodeCharacter
{
    [ApiController]
    [Route("rickandmorty/episodes/{episodeId}")]
    public class EpisodeCharactersController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string rickAndMortyApiUrl;
        public EpisodeCharactersController(HttpHelper httpHelper, IConfiguration configuration)
        {
            this.httpHelper = httpHelper;
            this.rickAndMortyApiUrl = configuration.GetValue<string>("Services:RickAndMortyApi");
        }

        [HttpGet]
        [Route("characters")]
        //[Authorizable("EpisodeCharacters_List")]
        //[RequiredHeaderParameters("Token")]
        public async Task<ActionResult<GetEpisodeCharactersResponse>> GetEpisodeCharacters(int episodeId, [FromQuery] GetEpisodeCharactersRequest request)
        {
            GetEpisodeCharactersResponse response = new GetEpisodeCharactersResponse();
            string url = ($"{rickAndMortyApiUrl}/rickandmorty/episodes/{episodeId}/characters?pageNumber=" + request.pageNumber + "&pageCount=" + request.pageCount);
            response = await httpHelper.Get<GetEpisodeCharactersResponse>(url);
            return new JsonResult(response);
        }
    }
}
