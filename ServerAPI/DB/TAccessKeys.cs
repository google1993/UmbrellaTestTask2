using System.ComponentModel.DataAnnotations;
using System;

namespace ServerAPI.DB
{
    public class AccessKeys
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public DateTime Create { get; set; }
        public DateTime LastAccess { get; set; }
        public bool IsPermanent { get; set; }

        public virtual Users User { get; set; }
    }
}
