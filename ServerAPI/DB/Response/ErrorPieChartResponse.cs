using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ServerAPI.DB.Response
{
    public class ErrorPieChartResponse
    {
        public string Version { get; set; } = String.Empty;
        public int ErrorsCount { get; set; }
    }
}
