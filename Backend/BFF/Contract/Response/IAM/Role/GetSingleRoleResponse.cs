using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.IAM.Role
{
    public class GetSingleRoleResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<string> claims { get; set; }
    }
}
