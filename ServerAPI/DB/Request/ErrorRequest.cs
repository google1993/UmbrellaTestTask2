
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerAPI.DB.Request
{
    public class ErrorsGetRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ErrorsCount { get; set; }
    }
}
