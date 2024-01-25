using Contract.Request;
using Contract.Request.IAM.User;
using Contract.Request.Users;
using Contract.Response;
using Contract.Response.IAM.User;
using Contract.Response.Users;
using Host.Filters;
using Host.Helpers;
using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Host.Controllers
{
    [ApiController]
    [Route("iam/users")]
    public class UsersController : ControllerBase
    {
        private readonly HttpHelper httpHelper;
        private readonly string iamUrl;

        public UsersController(HttpHelper httpHelper,IConfiguration configuration)
        {
            this.iamUrl = configuration.GetValue<string>("Services:IAM");
            this.httpHelper = httpHelper;
        }

        [HttpGet]
        [Authorizable("Users_List")]
        [RequiredHeaderParameters("Token")]
        public async Task<ActionResult<SearchUsersResponse>> Search([FromQuery] SearchUsersRequest request)
        {
            //burada iam'in search metodunu çağırıp response alıcaz.
            //aldığımız response'u return edicez.

            #region Url Builder

            //UriBuilder uriBuilder = new UriBuilder("http://localhost:5002/iam/users");
            //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query["firstName"] = request.firstName;
            //query["lastName"] = request.lastName;
            //query["userName"] = request.userName;
            //query["email"] = request.email;
            //uriBuilder.Query = query.ToString();

            #endregion

            SearchUsersResponse response = new SearchUsersResponse();
            string url = ($"{iamUrl}/iam/users?firstName={request.firstName}&lastName={request.lastName}&userName={request.userName}&email={request.email}");
            response = await httpHelper.Get<SearchUsersResponse>(url);

            ////Url Builder Bu url'i oluşturabilmek için yapıyoruz. Query parametresi istiyorsa UriBuilder yapılır.

            //using (HttpClient client = new HttpClient())
            //{
            //    // HTTP GET isteği göndermek için aşağıdaki örnek kullanabilirsiniz.
            //    HttpResponseMessage httpResponseMessage = await client.GetAsync(Url);

            //    // Yanıt başarılıysa işleme devam edin
            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        // Yanıtın içeriğini JSON formatından string olarak alın
            //        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            //        //responseContent diye bir değişken tanımladım. İlk satırda o değişkeni okudum.
            //        //Değişken json tipinde olduğu için o değişken deserialize ederek nesneye dönüştürdüm.
            //        // JSON veriyi DTO nesnesine dönüştürün
            //        response = JsonConvert.DeserializeObject<SearchUsersResponse>(responseContent);

            //        // DTO nesnesini geri döndürün
            //    }
            //    else
            //    {
            //        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            //        dynamic error = JsonConvert.DeserializeObject(responseContent);
            //        throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
            //    }
            //}

            return new JsonResult(response);
        }

        [Authorizable("Users_GetSingle")]
        [RequiredHeaderParameters("Token")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSingleUsersResponse>> GetSingle(int id)
        {
            GetSingleUsersResponse response = new GetSingleUsersResponse();

            response = await httpHelper.Get<GetSingleUsersResponse>($"{iamUrl}/iam/users/{id}");
            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage httpResponseMessage = await client.GetAsync($"http://localhost:5002/iam/users/{id}"); //HTTP GET isteği gönderdik.

            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            //        response = JsonConvert.DeserializeObject<GetSingleUsersResponse>(responseContent);
            //    }
            //    else
            //    {
            //        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            //        dynamic error = JsonConvert.DeserializeObject(responseContent);
            //        throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
            //    }
            //}

            return new JsonResult(response);
        }

        [Authorizable("Users_Create")]
        [RequiredHeaderParameters("Token")]
        [HttpPost]
        public async Task Create([FromBody] CreateUsersRequest request)
        {
            await httpHelper.Create($"{iamUrl}/iam/users", request);
        }

        [Authorizable("Users_Update")]
        [RequiredHeaderParameters("Token")]
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UpdateUsersRequest request)
        {
            await httpHelper.Update($"{iamUrl}/iam/users/{id}", request);
        }

        [Authorizable("Users_Delete")]
        [RequiredHeaderParameters("Token")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await httpHelper.Delete($"{iamUrl}/iam/users/{id}");
        }
    }
}
