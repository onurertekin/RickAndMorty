using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.Authentication
{
    public class AuthenticationRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
