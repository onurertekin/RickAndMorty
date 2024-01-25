using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.Episodes
{
    public class SearchEpisodesRequest
    {
        public string? episodeNo { get; set; }
        public string? name { get; set; }
        public DateTime airDate { get; set; }
        public DateTime createdOn { get; set; }
    }
}
