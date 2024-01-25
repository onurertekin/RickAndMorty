using DatabaseModel;
using DatabaseModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class EpisodeOperations
    {
        private readonly MainDbContext mainDbContext;
        public EpisodeOperations(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<Episode> Search(string name)
        {
            var query = mainDbContext.Episodes.AsQueryable();
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name == name);

            return query.ToList();
        }

    }
}
