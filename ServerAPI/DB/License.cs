using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.DB
{
    public class License
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [StringLength(17)]
        public string? MacAddress { get; set; }
        public virtual ICollection<LicenseKey> LicenseKeys { get; set; }

        public DateTime? ApearDate { get; set; } = DateTime.UtcNow;
    }
}
