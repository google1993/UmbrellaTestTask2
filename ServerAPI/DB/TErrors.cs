using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ServerAPI.DB
{
    public class Errors
    {
        [Key]
        public int Id { get; set; }
        public string ErrorGUID { get; set; } = String.Empty;
        public string Version { get; set; } = String.Empty;
        public string ErrorMsg { get; set; } = String.Empty;
        public string ErrorCallStack { get; set; } = String.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdate { get; set; }

        public string? LicenseKey { get; set; }
        public string? MacAddress { get; set; }
        public string? PersonMsg { get; set; }

        public virtual Dumps? Dump { get; set; }
        public virtual ICollection<Pics> Pics { get; set; } = [];
    }
}
