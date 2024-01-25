using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.GeekYaparApi.Content
{
    public class GetSingleContentResponse
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? author { get; set; }
        public int categoryId { get; set; }
    }
}
