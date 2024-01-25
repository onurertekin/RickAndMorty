namespace Contract.Response.Claims
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
        public List<Claim> claims { get;set; }
    }
}
