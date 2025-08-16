using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ServerAPI.DB.Response
{
    public class ErrorResponse
    {
        public string ErrorGUID { get; set; } = String.Empty;
        public string Version { get; set; } = String.Empty;
        public string ErrorMsg { get; set; } = String.Empty;
        public string ErrorCallStack { get; set; } = String.Empty;
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
