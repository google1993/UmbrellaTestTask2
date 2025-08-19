using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models.Request
{
    public class ErrorGetRequest
    {
        public string ErrorGUID { get; set; } = string.Empty;
    }
}
