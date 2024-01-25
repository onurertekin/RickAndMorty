using DatabaseModel;
using DatabaseModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class ClaimOperation
    {
        private readonly MainDbContext mainDbContext;
        public ClaimOperation(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Claim> Search(string code)
        {
            var query = mainDbContext.Claims.AsQueryable();
            if (!string.IsNullOrEmpty(code))
                query = query.Where(x => x.Code == code);

            return query.ToList();
        }
    }
}
