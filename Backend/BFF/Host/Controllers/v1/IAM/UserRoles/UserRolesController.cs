using Contract.Request.IAM.UsersRoles;
using Contract.Response.IAM.UsersRoles;
using Host.Helpers;
using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Host.Controllers.v1.IAM.UserRoles
{
    [ApiController]
    [Route("iam/users/{userId}")]
    public class UserRolesController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string iamUrl;
        public UserRolesController(HttpHelper httpHelper,IConfiguration configuration)
        {
            this.iamUrl = configuration.GetValue<string>("Services:IAM");
            this.httpHelper = httpHelper;
        }

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult<GetUserRolesResponse>> GetUserRoles([FromRoute] int userId)
        {
            GetUserRolesResponse response = new GetUserRolesResponse();
            response = await httpHelper.Get<GetUserRolesResponse>($"{iamUrl}/iam/users/{userId}/roles");
            return new JsonResult(response);
        }

        [HttpPut]
        [Route("roles")]
        public async Task Update(int userId, [FromBody] UserRolesUpdateRequest request)
        {
            await httpHelper.Update($"{iamUrl}/iam/users/{userId}/roles",request);
        }
    }
}
