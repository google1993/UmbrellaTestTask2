using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models.Request
{
    public class ErrorGetRequest
    {
        [Key]
        public string ErrorGUID { get; set; } = string.Empty;
    }
}
