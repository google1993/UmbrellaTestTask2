using System;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string TgUsername { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? MessageId { get; set; }
        public string ChatId { get; set; }

    }
}
