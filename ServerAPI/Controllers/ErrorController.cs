using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.DB;
using ServerAPI.Models.Request;
using ServerAPI.Models.Response;
using System.Linq;
using System.Text.Json;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController(MainContext context
        ) : ControllerBase
    {
        private readonly MainContext _context = context;

        [HttpPost("get_error")]
        public async Task<IActionResult> GetError([FromBody] JsonElement requestBody)
        {
            var request = new ErrorGetRequest();
            try
            {
                request = JsonSerializer.Deserialize<ErrorGetRequest>(requestBody.ToString() ?? "{}");
                if (request == null)
                {
                    throw new JsonException("Request is null.");
                }
            }
            catch (JsonException ex)
            {
                return BadRequest(new { Message = "Error deserialization: " + ex.Message });
            }

            var errorsQuery = _context.Errors.AsQueryable();
            var results = await errorsQuery
                .Where(e => e.ErrorGUID == request.ErrorGUID)
                .Select(ErrorResponse.ErrorResponseExpression).ToListAsync();
                
                
            return Ok(new
            {
                resultData = results
            });
        }
        [HttpPost("get_list_error")]
        public async Task<IActionResult> GetListError([FromBody] JsonElement requestBody)
        {
            var request = new ErrorsGetRequest();
            try
            {
                request = JsonSerializer.Deserialize<ErrorsGetRequest>(requestBody.ToString() ?? "{}");
                if (request == null)
                {
                    throw new JsonException("Request is null.");
                }
            }
            catch (JsonException ex)
            {
                return BadRequest(new { Message = "Error deserialization: " + ex.Message});
            }
            var errorsQuery = _context.Errors.AsQueryable();
            if(request.StartDate != null)
            {
                errorsQuery = errorsQuery.Where(errorsQuery => errorsQuery.CreateTime >= request.StartDate);
            }
            if(request.EndDate != null)
            {
                errorsQuery = errorsQuery.Where(errorsQuery => errorsQuery.CreateTime <= request.EndDate);
            }
            var results = await errorsQuery
                .Select(ErrorResponse.ErrorResponseExpression)
                .ToListAsync();
            return Ok(new
            {
               resultData = results
            });
        }
        [HttpPost("get_list_errors_pie")]
        public async Task<IActionResult> GetListErrorPieChart([FromBody] JsonElement requestBody)
        {
            var request = new ErrorsGetRequest();
            try
            {
                request = JsonSerializer.Deserialize<ErrorsGetRequest>(requestBody.ToString() ?? "{}");
                if (request == null)
                {
                    throw new JsonException("Request is null.");
                }
            }
            catch (JsonException ex)
            {
                return BadRequest(new { Message = "Error deserialization: " + ex.Message });
            }
            var errorsQuery = _context.Errors.AsQueryable();
            if (request.StartDate != null)
            {
                errorsQuery = errorsQuery.Where(errorsQuery => errorsQuery.CreateTime >= request.StartDate);
            }
            if (request.EndDate != null)
            {
                errorsQuery = errorsQuery.Where(errorsQuery => errorsQuery.CreateTime <= request.EndDate);
            }
            var results = await errorsQuery
                .GroupBy(e => e.Version)
                .Select(g => new ErrorPieChartResponse
                {
                    Version = (g.Key).Substring(0,8),
                    ErrorsCount = g.Count()
                }).ToListAsync();
            return Ok(new
            {
                resultData = results
            });
        }
    }
}
