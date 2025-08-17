using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ServerAPI.Models.Response
{
    public class ErrorPieChartResponse
    {
        public string Version { get; set; } = string.Empty;
        public int ErrorsCount { get; set; }
    }
}
