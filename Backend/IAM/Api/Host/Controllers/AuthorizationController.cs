using Contract.Request.Authentication;
using Contract.Request.Authorization;
using Contract.Response.Authentication;
using DomainService.Exceptions;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("iam")]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserRolesOperation userRolesOperation;
        public AuthorizationController(UserRolesOperation userRolesOperation)
        {
            this.userRolesOperation = userRolesOperation;
        }

        [HttpPost]
        [Route("authorization")]
        public void Authorization([FromBody] AuthorizationRequest request)
        {
            var hasRole = userRolesOperation.CheckUserClaim(request.userId, request.claimCode);
            if (!hasRole)
                throw new BusinessException(401, "Unauthorized");
        }
    }
}
