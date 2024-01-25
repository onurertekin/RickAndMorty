using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.IAM.Claim
{
    public class SearchClaimsResponse
    {
        public class Claim
        {
            public int id { get; set; }
            public string code { get; set; }
        }
        public SearchClaimsResponse()
        {
            claims = new List<Claim>();
        }
        public List<Claim> claims { get; set; }
    }
}
