using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB.Response
{
    public class ErrorResponse
    {
        [Key]
        public int Id { get; set; }
        public string ErrorGUID { get; set; } = String.Empty;
        public string Version { get; set; } = String.Empty;
        public string ErrorMsg { get; set; } = String.Empty;
        public string ErrorCallStack { get; set; } = String.Empty;
        public DateTime CreateTime { get; set; }
        public string? MacAddress { get; set; }
        public virtual Dumps? Dump { get; set; }
    }
}
