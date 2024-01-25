using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class EpisodeCharactersOperations
    {
        private readonly MainDbContext mainDbContext;
        public EpisodeCharactersOperations(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public IList<Character> GetEpisodeCharacters(int episodeId, int pageNumber, int pageCount)
        {
            var episode = mainDbContext.Episodes.Include(x => x.Characters).Where(x => x.Id == episodeId).SingleOrDefault();
            if (episode == null)
                throw new BusinessException(404, "Bölüm bulunamadı.");

            return episode.Characters.Skip(pageNumber - 1).Take(pageCount).ToList();

        }
    }
}
