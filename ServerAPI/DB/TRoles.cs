using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Permissions> Permissions { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
