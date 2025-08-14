using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public long ChatId { get; set; } 
        public virtual ICollection<AccessKeys> AccessKeys { get; set; } = [];
        public virtual ICollection<Roles> Roles { get; set; } = [];
        public virtual ICollection<Permissions> Permissions { get; set; } = [];
    }
}
