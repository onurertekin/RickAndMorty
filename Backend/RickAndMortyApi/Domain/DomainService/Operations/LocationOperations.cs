using DatabaseModel;
using DatabaseModel.Entities;

namespace DomainService.Operations
{
    public class LocationOperations
    {
        private readonly MainDbContext mainDbContext;
        public LocationOperations(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<Location> Search(string name, string type, string dimension)
        {
            var query = mainDbContext.Locations.AsQueryable();
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name == name);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.Type == type);
            if (!string.IsNullOrEmpty(dimension))
                query = query.Where(x => x.Dimension == dimension);

            return query.ToList();
        }
    }
}
