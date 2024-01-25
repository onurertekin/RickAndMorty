using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.Characters
{
    public class SearchCharactersResponse
    {
        public class Characters
        {
            public int id { get; set; }
            public string name { get; set; }
            public int status { get; set; }
            public int gender { get; set; }
            public int species { get; set; }
            public int locationId { get; set; }
            public int originId { get; set; }
        }
        public SearchCharactersResponse()
        {
            characters = new List<Characters>(); 
        }
        public List<Characters> characters { get; set; }
    }
}
