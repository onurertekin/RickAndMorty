using Contract.Request.IAM.Authentication;
using Contract.Response.IAM.Authentication;
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

namespace Host.Controllers.v1.IAM.Authentication
{
    [ApiController]
    [Route("iam")]
    public class AuthenticationController : ControllerBase
    {
        private readonly string iamUrl;
        public AuthenticationController(IConfiguration configuration)
        {
            this.iamUrl = configuration.GetValue<string>("Services:IAM");
        }
        [HttpPost]
        [Route("authentication")]
        public async Task<ActionResult<AuthenticationResponse>> Authentication([FromBody] AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync($"{iamUrl}/iam/authentication", content); //content ile body'i gönderiyoruz.
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);
                }
                else
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync(); //bodyi okuduk
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    //StatusCode int bir değer olmadığı için (int) yapıyoruz.
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString()); 
                }

            }
            return new JsonResult(response);
        }
    }
}
