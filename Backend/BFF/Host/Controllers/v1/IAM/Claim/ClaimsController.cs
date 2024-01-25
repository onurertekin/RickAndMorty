using Contract.Request.IAM.Claim;
using Contract.Response.IAM.Claim;
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
using System.Web;

namespace Host.Controllers.v1.IAM.Claim
{
    [ApiController]
    [Route("iam/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string iamUrl;
        public ClaimsController(HttpHelper httpHelper,IConfiguration configuration)
        {
            this.iamUrl = configuration.GetValue<string>("Services:IAM");
            this.httpHelper = httpHelper;
        }
        [HttpGet]
        public async Task<ActionResult<SearchClaimsResponse>> Search([FromQuery] SearchClaimsRequest request)
        {
            SearchClaimsResponse response = new SearchClaimsResponse();
            response = await httpHelper.Get<SearchClaimsResponse>($"{iamUrl}/iam/claims?code={request.code}");
            return new JsonResult(response);
        }
    }
}
