using Contract.Request.IAM.Role;
using Contract.Response.IAM.Role;
using Host.Filters;
using Host.Helpers;
using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Host.Controllers.v1.IAM.Role
{
    [ApiController]
    [Route("iam/roles")]
    public class RolesController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string iamUrl;

        public RolesController(HttpHelper httpHelper,IConfiguration configuration)
        {
            this.iamUrl = configuration.GetValue<string>("Services:IAM");
            this.httpHelper = httpHelper;
        }

        [Authorizable("Roles_List")]
        [RequiredHeaderParameters("Token")]
        [HttpGet]
        public async Task<ActionResult<SearchRolesResponse>> Search([FromQuery] SearchRolesRequest request)
        {
            SearchRolesResponse response = new SearchRolesResponse();
            string url = ($"{iamUrl}/iam/roles?name={request.name}");
            response = await httpHelper.Get<SearchRolesResponse>(url);

            return new JsonResult(response);
        }

        [Authorizable("Roles_GetSingle")]
        [RequiredHeaderParameters("Token")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSingleRoleResponse>> GetSingle(int id)
        {
            GetSingleRoleResponse response = new GetSingleRoleResponse();
            response = await httpHelper.Get<GetSingleRoleResponse>($"{iamUrl}/iam/roles/{id}");
            return new JsonResult(response);
        }

        [Authorizable("Roles_Create")]
        [RequiredHeaderParameters("Token")]
        [HttpPost]
        public async Task Create([FromBody] CreateRoleRequest request)
        {
            await httpHelper.Create($"{iamUrl}/iam/roles", request);
        }

        [Authorizable("Roles_Update")]
        [RequiredHeaderParameters("Token")]
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UpdateRoleRequest request)
        {
            await httpHelper.Update($"{iamUrl}/iam/roles/{id}",request);
        }

        [Authorizable("Roles_Delete")]
        [RequiredHeaderParameters("Token")]
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await httpHelper.Delete($"{iamUrl}/iam/roles/{id}");
        }
    }
}
