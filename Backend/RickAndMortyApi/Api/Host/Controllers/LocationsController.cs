using Contract.Request.Locations;
using Contract.Response.Locations;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rickandmorty/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly LocationOperations locationOperations;
        public LocationsController(LocationOperations locationOperations)
        {
            this.locationOperations = locationOperations;
        }

        [HttpGet]
        public ActionResult<SearchLocationsResponse> Search([FromQuery] SearchLocationsRequest request)
        {
            var locations = locationOperations.Search(request.name, request.type, request.dimension);
            SearchLocationsResponse response = new SearchLocationsResponse();
            foreach (var location in locations)
            {
                response.locations.Add(new SearchLocationsResponse.Locations()
                {
                    id= location.Id,
                    name = location.Name,
                    type = location.Type,
                    dimension = location.Dimension,
                });
            }
            return new JsonResult(response);
        }
    }
}
