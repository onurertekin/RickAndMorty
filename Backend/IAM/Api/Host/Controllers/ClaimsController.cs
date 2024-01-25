using Contract.Request.Claims;
using Contract.Response.Claims;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("iam/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimOperation claimOperation;
        public ClaimsController(ClaimOperation actionOperation)
        {
            this.claimOperation = actionOperation;
        }
        [HttpGet]
        public ActionResult<SearchClaimsResponse> Search([FromQuery] SearchClaimsRequest request)
        {
            var actions = claimOperation.Search(request.code);
            SearchClaimsResponse response = new SearchClaimsResponse();
            foreach (var action in actions)
            {
                response.claims.Add(new SearchClaimsResponse.Claim()
                {
                    id = action.Id,
                    code = action.Code
                });
            }
            return new JsonResult(response);
        }
    }
}
