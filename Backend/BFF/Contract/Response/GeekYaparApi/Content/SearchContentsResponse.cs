using System.Collections.Generic;

namespace Contract.Response.GeekYaparApi.Content
{
    public class SearchContentsResponse
    {
        public class Contents
        {
            public int id { get; set; }
            public string? name { get; set; }
            public string? description { get; set; }
            public string? author { get; set; }
            public int categoryId { get; set; }
        }
        public SearchContentsResponse()
        {
            contents = new List<Contents>();
        }
        public List<Contents> contents { get; set; }

    }
}

