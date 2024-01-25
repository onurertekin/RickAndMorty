using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.Characters
{
    public class GetSingleCharacterResponse
    {
        public string? name { get; set; }
        public int status { get; set; }
        public int gender { get; set; }
        public int species { get; set; }
        public int locationId { get; set; }
        public int originId { get; set; }
    }
}
