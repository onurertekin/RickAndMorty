using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.Roles
{
    public class SearchRolesResponse
    {
        public class Role 
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public SearchRolesResponse()
        {
            roles = new List<Role>();
        }
        public List<Role> roles { get; set; }
    }
}
