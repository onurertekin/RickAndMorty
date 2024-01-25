using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.IAM.UsersRoles
{
    public class GetUserRolesResponse
    {
        public class UserRoles
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public GetUserRolesResponse()
        {
            roles = new List<UserRoles>();
        }
        public List<UserRoles> roles { get; set; }
    }
}
