using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.Users
{
    public class SearchUsersResponse
    {
        public class User
        {
            public int id { get; set; }
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? email { get; set; }
            public string? userName { get; set; }
            public string? password { get; set; }
        }
        public SearchUsersResponse()
        {
            users = new List<User>();
        }
        public List<User> users { get; set; }
    }
}
