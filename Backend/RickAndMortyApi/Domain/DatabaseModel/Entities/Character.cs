using DatabaseModel.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel.Entities
{
    [Table("Characters")]
    public class Character
    {
        public Character()
        {
            Episodes = new HashSet<Episode>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public CharacterStatus Status { get; set; }
        public GendersType Gender { get; set; }
        public SpeciesType Species { get; set; }
        public int LocationId { get; set; }
        public int OriginId { get; set; }

        public virtual ISet<Episode> Episodes { get; set; }


    }
}
