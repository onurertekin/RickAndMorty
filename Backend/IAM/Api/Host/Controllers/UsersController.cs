using Azure.Core;
using Contract.Request.Users;
using Contract.Response.Users;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;
using Nova.Core.Attributes;

namespace Host.Controllers
{
    [ApiController]
    [Route("IAM/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserOperation userOperation;
        public UsersController(UserOperation userOperation)
        {
            this.userOperation = userOperation;
        }

        [HttpGet]
        public ActionResult<SearchUsersResponse> Search([FromQuery] SearchUsersRequest request)
        {
            var users = userOperation.Search(request.userName, request.firstName, request.lastName, request.email);

            SearchUsersResponse response = new SearchUsersResponse();

            foreach (var user in users)
            {
                response.users.Add(new SearchUsersResponse.User()
                {
                    id = user.Id,
                    userName = user.UserName,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    password = user.Password
                });
            }

            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GetSingleUsersResponse> GetSingle(int id)
        {
            var user = userOperation.GetSingle(id);

            GetSingleUsersResponse response = new GetSingleUsersResponse();
            response.id = user.Id;
            response.userName = user.UserName;
            response.firstName = user.FirstName;
            response.lastName = user.LastName;
            response.email = user.Email;
            response.password = user.Password;

            return new JsonResult(response);
        }

        [HttpPost]
        public void Create([FromBody] CreateUsersRequest request)
        {
            userOperation.Create(request.firstName, request.lastName, request.userName, request.email, request.password);
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] UpdateUsersRequest request)
        {
            userOperation.Update(id, request.firstName, request.lastName, request.userName, request.email, request.password);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userOperation.Delete(id);
        }
    }
}
