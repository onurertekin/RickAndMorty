using Contract.Request.RickAndMortyApi.Character;
using Contract.Response.RickAndMortyApi.Character;
using Host.Filters;
using Host.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Controllers.v1.RickAndMortyApi.Character
{
    [ApiController]
    [Route("rickandmorty/characters")]
    public class CharactersController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string rickAndMortyApiUrl;

        public CharactersController(HttpHelper httpHelper, IConfiguration configuration)
        {
            this.httpHelper = httpHelper;
            this.rickAndMortyApiUrl = configuration.GetValue<string>("Services:RickAndMortyApi");
        }

        [HttpGet]
        //[Authorizable("Characters_List")]
        //[RequiredHeaderParameters("Token")]
        public async Task<ActionResult<SearchCharactersResponse>> Search([FromQuery] SearchCharactersRequest request)
        {
            SearchCharactersResponse response = new SearchCharactersResponse();
            string url = ($"{rickAndMortyApiUrl}/rickandmorty/characters?pageNumber={request.pageNumber}&pageCount={request.pageCount}&name={request.name}&status={request.status}&gender={request.gender}&species={request.species}&locationId={request.locationId}&originId={request.originId}");
            response = await httpHelper.Get<SearchCharactersResponse>(url);
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        //[Authorizable("Characters_GetSingle")]
        //[RequiredHeaderParameters("Token")]
        public async Task<ActionResult<GetSingleCharacterResponse>> GetSingle(int id)
        {
            GetSingleCharacterResponse response = new GetSingleCharacterResponse();
            string url = ($"{rickAndMortyApiUrl}/rickandmorty/characters/{id}");
            response = await httpHelper.Get<GetSingleCharacterResponse>(url);
            return new JsonResult(response);
        }
    }
}
