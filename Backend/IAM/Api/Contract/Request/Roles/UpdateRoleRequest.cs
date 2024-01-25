using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.Roles
{
    public class UpdateRoleRequest
    {
        public string name { get; set; }
        public List<string> claims { get; set; }
    }
}
