using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.DB.Response
{
    public class ErrorPieChartResponse 
    {
        [Key]
        public int Id { get; set; }
        public string Version { get; set; } = String.Empty;
        public int ErrorsCount { get; set; }
    }
}
