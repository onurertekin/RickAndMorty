using Contract.Request.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Entities
{
    [Table("Roles")]
    public class Role
    {
        public Role() 
        {
            Users = new HashSet<User>();
            Claims= new HashSet<Claim>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ISet<User> Users { get; set;}
        public virtual ISet<Claim> Claims { get; set;}
    }
}
