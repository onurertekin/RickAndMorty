using DatabaseModel;
using DatabaseModel.Entities;
using DatabaseModel.Enumerations;
using DomainService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class CharacterOperations
    {
        private readonly MainDbContext mainDbContext;
        public CharacterOperations(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<Character> Search(string name, int? status, int? gender, int? species, int? locationId, int pageNumber, int pageCount)
        {
            var query = mainDbContext.Characters.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => EF.Functions.Like(e.Name, $"%{name}%"));

            if (status != null)
                query = query.Where(x => x.Status == (CharacterStatus)status);

            if (gender != null)
                query = query.Where(x => x.Gender == (GendersType)gender);

            if (species != null)
                query = query.Where(x => x.Species == (SpeciesType)species);

            if (locationId != null)
                query = query.Where(x => x.LocationId == locationId);

            query = query.Skip(pageNumber - 1).Take(pageCount);

            return query.ToList();
        }

        public Character GetSingle(int id)
        {
            var character = mainDbContext.Characters.Where(x => x.Id == id).SingleOrDefault();
            if (character == null)
                throw new BusinessException(404, "Karakter bulunamadı.");

            return character;
        }
    }
}
