using Contract.Request.UsersRoles;
using Contract.Response.UsersRoles;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("IAM/users/{userId}")]
    public class UserRolesController : ControllerBase
    {
        private readonly UserRolesOperation userRolesOperation;
        public UserRolesController(UserRolesOperation userRolesOperation)
        {
            this.userRolesOperation = userRolesOperation;
        }
        [HttpGet]
        [Route("roles")]
        public ActionResult<GetUserRolesResponse> GetUserRoles([FromRoute] int userId)
        {
            var roles = userRolesOperation.GetUserRoles(userId);
            GetUserRolesResponse response = new GetUserRolesResponse();
            foreach (var role in roles)
            {
                response.roles.Add(new GetUserRolesResponse.UserRoles()
                {
                    id = role.Id,
                    name = role.Name,
                });
            }
            return new JsonResult(response);
        }
        [HttpPut]
        [Route("roles")]
        public void Update(int userId,[FromBody] UserRolesUpdateRequest request)
        {
            userRolesOperation.Update(userId,request.roles);
        }
    }
}
