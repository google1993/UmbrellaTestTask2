using Microsoft.AspNetCore.Mvc;
using ServerAPI.DB;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ServerAPI.Models.Response
{
    public class ErrorResponse
    {
        public string ErrorGUID { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string ErrorMsg { get; set; } = string.Empty;
        public string ErrorCallStack { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public string? MacAddress { get; set; }

        public static Expression<Func<Errors, ErrorResponse>> ErrorResponseExpression =>
            e => new ErrorResponse
            {
                ErrorGUID = e.ErrorGUID,
                ErrorMsg = e.ErrorMsg,
                ErrorCallStack = e.ErrorCallStack,  
                CreateTime = e.CreateTime,
                MacAddress = e.MacAddress,
            };
    }
}
