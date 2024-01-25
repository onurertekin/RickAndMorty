using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Entities
{

    [Table("Episodes")]
    public class Episode
    {
        public Episode()
        { 
            Characters = new HashSet<Character>();
        }
        public int Id { get; set; }
        public string EpisodeNo { get; set; }
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ISet<Character> Characters { get; set; }
    }
}
