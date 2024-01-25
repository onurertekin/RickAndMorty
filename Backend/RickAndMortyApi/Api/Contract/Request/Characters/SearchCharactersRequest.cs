using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.Characters
{
    public class SearchCharactersRequest
    {
        public string? name { get; set; }
        public int? status { get; set; }
        public int? gender { get; set; }
        public int? species { get; set; }
        public int? locationId { get; set; }
        public int? originId { get; set; }

        public int pageNumber { get; set; }
        public int pageCount { get; set; }
    }
}
