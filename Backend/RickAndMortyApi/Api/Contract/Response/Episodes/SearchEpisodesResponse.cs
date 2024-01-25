using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.Episodes
{
    public class SearchEpisodesResponse
    {
        public class Episodes
        {
            public int id { get; set; }
            public string episodeNo { get; set; }
            public string name { get; set; }
            public DateTime airDate { get; set; }
            public DateTime createdOn { get; set; }
        }
        public SearchEpisodesResponse()
        {
            episodes= new List<Episodes>();  
        }
        public List<Episodes> episodes { get;set; }
    }
}
