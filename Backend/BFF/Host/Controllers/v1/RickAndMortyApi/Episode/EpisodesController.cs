using Contract.Request.RickAndMortyApi.Character;
using Contract.Request.RickAndMortyApi.Episode;
using Contract.Response.RickAndMortyApi.Character;
using Contract.Response.RickAndMortyApi.Episode;
using Host.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Controllers.v1.RickAndMortyApi.Episode
{
    [ApiController]
    [Route("rickandmorty/episodes")]
    public class EpisodesController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string rickAndMortyApiUrl;

        public EpisodesController(HttpHelper httpHelper, IConfiguration configuration)
        {
            this.httpHelper = httpHelper;
            this.rickAndMortyApiUrl = configuration.GetValue<string>("Services:RickAndMortyApi");
        }

        [HttpGet]
        //[Authorizable("Episodes_List")]
        //[RequiredHeaderParameters("Token")]
        public async Task<ActionResult<SearchEpisodesResponse>> Search([FromQuery] SearchEpisodesRequest request)
        {
            SearchEpisodesResponse response = new SearchEpisodesResponse();
            string url = ($"{rickAndMortyApiUrl}/rickandmorty/episodes?name={request.name}&episodeNo={request.episodeNo}&airDate={request.airDate}&createdOn={request.createdOn}");
            response = await httpHelper.Get<SearchEpisodesResponse>(url);
            return new JsonResult(response);
        }
    }
}
