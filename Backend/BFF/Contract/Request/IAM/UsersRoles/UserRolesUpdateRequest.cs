using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.IAM.UsersRoles
{
    public class UserRolesUpdateRequest
    {
        public List<int> roles { get; set; }
    }
}
