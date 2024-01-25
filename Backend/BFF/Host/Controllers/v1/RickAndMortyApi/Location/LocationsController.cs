using Contract.Request.RickAndMortyApi.Character;
using Contract.Request.RickAndMortyApi.Location;
using Contract.Response.RickAndMortyApi.Character;
using Contract.Response.RickAndMortyApi.Location;
using Host.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Controllers.v1.RickAndMortyApi.Location
{
    [ApiController]
    [Route("rickandmorty/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string rickAndMortyApiUrl;
        public LocationsController(HttpHelper httpHelper, IConfiguration configuration)
        {
            this.httpHelper = httpHelper;
            this.rickAndMortyApiUrl = configuration.GetValue<string>("Services:RickAndMortyApi");
        }

        [HttpGet]
        //[Authorizable("Locations_List")]
        //[RequiredHeaderParameters("Token")]
        public async Task<ActionResult<SearchLocationsResponse>> Search([FromQuery] SearchLocationsRequest request)
        {
            SearchLocationsResponse response = new SearchLocationsResponse();
            string url = ($"{rickAndMortyApiUrl}/rickandmorty/locations?name={request.name}&type={request.type}&dimension={request.dimension}");
            response = await httpHelper.Get<SearchLocationsResponse>(url);

            return new JsonResult(response);
        }
    }
}
