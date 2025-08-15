using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB.Request
{
    public class ErrorGetRequest
    {
        [Key]
        public string ErrorGUID { get; set; } = String.Empty;
    }
}
