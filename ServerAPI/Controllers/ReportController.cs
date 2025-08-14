using Microsoft.AspNetCore.Mvc;
using ServerAPI.DB;
using System.Security.Principal;
using System.Text.Json;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("adv")]
    public class ReportController(
        ILogger<ReportController> log,
        MainContext mainContext
        ) : ControllerBase
    {
        private readonly ILogger<ReportController> _log = log;
        private readonly MainContext _mainCtx = mainContext;

        [HttpPost("all")]
        public async Task<IActionResult> All([FromBody] JsonElement model)
        {
            // Заглушка
            await Task.Run(() => { _log.LogInformation("Get request: {}", model.ToString()); });

            return Ok(new { Result = 0 });
        }
    }
}
