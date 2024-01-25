using Contract.Request.Authentication;
using Contract.Response.Authentication;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("iam")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationOperation authenticationOperation;
        public AuthenticationController(AuthenticationOperation authenticationOperation)
        {
            this.authenticationOperation = authenticationOperation;
        }

        [HttpPost]
        [Route("authentication")]
        public ActionResult<AuthenticationResponse> Authentication([FromBody] AuthenticationRequest request)
        {
            var token = authenticationOperation.Authentication(request.username, request.password);

            AuthenticationResponse response = new AuthenticationResponse();
            response.token = token;

            return new JsonResult(response);
        }
    }
}
