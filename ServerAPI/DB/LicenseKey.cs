using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.DB
{
    [Index("KeyValue", IsUnique = true, Name = "KeyValue_Index")]
    public class LicenseKey
    {
        [Key]
        public int Id { get; set; }
        public int? LicenseId { get; set; }

        public int DurationDays { get; set; } 

        [Required, StringLength(16)]
        public string KeyValue { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        public DateTime? ActivatedDate { get; set; }

        public long ChatId { get; set; }

        [ForeignKey("LicenseId")]
        public virtual License? License { get; set; }
    }

}
