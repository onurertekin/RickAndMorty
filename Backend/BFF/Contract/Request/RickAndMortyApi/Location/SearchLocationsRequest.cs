using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.RickAndMortyApi.Location
{
    public class SearchLocationsRequest
    {
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
    }
}
