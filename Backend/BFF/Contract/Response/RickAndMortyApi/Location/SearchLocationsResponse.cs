using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.RickAndMortyApi.Location
{
    public class SearchLocationsResponse
    {
        public class Locations
        {
            public int id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string dimension { get; set; }
        }
        public SearchLocationsResponse()
        {
            locations = new List<Locations>();
        }
        public List<Locations> locations { get; set; }
    }
}
