using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerAPI.Models.Request
{
    public class ErrorsGetRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ErrorsCount { get; set; }
    }
}
