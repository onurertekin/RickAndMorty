using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Entities
{
    [Table("Claims")]
    public class Claim
    {
        public Claim() 
        {
            Roles = new HashSet<Role>();
        } 
        public int Id { get; set; }
        public string Code { get; set; }
        
        public virtual ISet<Role> Roles { get; set; }
    }
}
