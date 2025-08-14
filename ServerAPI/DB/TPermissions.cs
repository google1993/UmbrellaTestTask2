using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
