using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.Authorization
{
    public class AuthorizationRequest
    {
        public int userId { get; set; }
        public string claimCode { get; set; }
    }
}
