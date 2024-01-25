using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.GeekYaparApi.Categories
{
    public class SearchCategoriesResponse
    {

        public class Categories
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public SearchCategoriesResponse()
        {
            categories = new List<Categories>();
        }

        public List<Categories> categories { get; set; }
    }
}
