using Contract.Request.Roles;
using Contract.Response.Roles;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("iam/roles")]
    public class RolesController : ControllerBase
    {
        private readonly RoleOperation roleOperation;

        public RolesController(RoleOperation roleOperation)
        {
            this.roleOperation = roleOperation;
        }
        [HttpGet]
        public ActionResult<SearchRolesResponse> Search([FromQuery] SearchRolesRequest request)
        {
            var roles = roleOperation.Search(request.name);
            SearchRolesResponse response = new SearchRolesResponse();
            foreach (var role in roles)
            {
                response.roles.Add(new SearchRolesResponse.Role()
                {
                    id = role.Id,
                    name = role.Name
                });
            }
            return new JsonResult(response);
        }
        [HttpGet("{id}")]
        public ActionResult<GetSingleRolesResponse> GetSingle(int id)
        {
            var roles = roleOperation.GetSingle(id);
            GetSingleRolesResponse response = new GetSingleRolesResponse();
            response.id = id;
            response.name = roles.Name;
            response.claims = roles.Claims.Select(x => x.Code).ToList();
            return new JsonResult(response);
        }
        [HttpPost]
        public void Create([FromBody] CreateRoleRequest request)
        {
            roleOperation.Create(request.name, request.claims);
        }
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] UpdateRoleRequest request)
        {
            roleOperation.Update(id, request.name, request.claims);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            roleOperation.Delete(id);
        }
    }
}
