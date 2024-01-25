using System.Collections.Generic;

namespace Contract.Response
{
    public class EnumerationResponse
    {
        public class Enumeration
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public IList<Enumeration> enumerations { get; set; }
    }
}