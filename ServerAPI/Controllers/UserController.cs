using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.DB;
using System.Data;
using System.Security.Principal;
using System.Text.Json;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(
        MainContext mainContext
        ) : ControllerBase
    {
        private readonly MainContext _mainCtx = mainContext;

        [HttpPost("auth-users-stats")]
        public async Task<IActionResult> GetAuthUsersStats()
        {
            var stats = await _mainCtx.Users
                .Select(
                u => new
                {
                    u.Login,

                    u.EMail,
                
                    //LicenseCount = _mainCtx.License
                    //    .Count(l => l.User.Id == u.Id),
                
                    LicenseKeysCount = _mainCtx.LicenseKey
                        .Count(k => k.License.User.Id == u.Id),
                
                    Devices = _mainCtx.License
                        .Count(d => d.User.Id == u.Id),

                    Companies = (from l in _mainCtx.License
                                 join ca in _mainCtx.UserCompanyActivation
                                     on l.MacAddress equals ca.Mac
                                 where l.User.Id == u.Id
                                 select new
                                 {
                                     ca.CompanyName,
                                     l.MacAddress
                                 }).ToList(),

                    TotalPayments = _mainCtx.Payments
                        .Where(p => p.TgUsername == u.Login)
                        .Sum(p => (decimal?)p.Amount) ?? 0
                }).ToListAsync();

            return Ok(stats);
        }

        [HttpPost("free-trial-licenses")]
        public async Task<IActionResult> GetFreeTriaLicenses()
        {
            var licenses = await _mainCtx.License
                .Where(l => l.User == null)
                .Select(
                l => new
                {
                    // l.Id,
                    l.MacAddress,
                    Company = _mainCtx.UserCompanyActivation
                     .Where(ca => ca.Mac == l.MacAddress)
                     .Select(ca => ca.CompanyName)
                     .FirstOrDefault(),
                    l.CreatedDate,
                    l.ExpirationDate,
                    l.ApearDate
                }).ToListAsync();

            return Ok(licenses);
        }

        [HttpPost("main-stats")]
        public async Task<IActionResult> GetMainStats()
        {
            var deviceCount = await _mainCtx.License
                .CountAsync();

            var activeDevices = await _mainCtx.License
                .CountAsync(ad => ad.ExpirationDate >= DateTime.UtcNow);

            var freeTrials = await _mainCtx.License
                .CountAsync(ft => ft.User == null);

            var authUsers = await _mainCtx.Users
                .CountAsync();

            var companies = await _mainCtx.UserCompanyActivation
                .GroupBy(ca => ca.CompanyName)
                .Select(g => new
                {
                    CompanyName = g.Key,
                    DeviceCount = g.Select(ca => ca.Mac).Distinct().Count()
                })
                .ToListAsync();

            var activatedKeys = await _mainCtx.LicenseKey
                .CountAsync(ak => ak.ActivatedDate != null);

            var totalPayment = _mainCtx.Payments
                .Sum(p => (decimal?)p.Amount) ?? 0;

            var mainStats = new
            {
                DeviceCount = deviceCount,
                ActiveDevices = activeDevices,
                FreeTrials = freeTrials,
                AuthUsers = authUsers,
                Companies = companies,
                ActivatedKeys = activatedKeys,
                TotalPayment = totalPayment
            };

            return Ok(mainStats);
        }

        [HttpPost("period-main-stats")]
        public async Task<IActionResult> GetPeriodMainStats([FromBody] JsonElement request)
        {
            var startDate = GetDate(request, "startDate");
            var endDate = GetDate(request, "endDate");

            var newDeviceCount = await _mainCtx.License
                .Where(l => l.ApearDate >= startDate && l.ApearDate <= endDate)
                .CountAsync();
            
            var activeDevices = await _mainCtx.License
                .CountAsync(ad => ad.CreatedDate <= endDate && ad.ExpirationDate >= startDate);
            
            var newFreeTrials = await _mainCtx.License
                .CountAsync(ft => ft.User == null && (ft.ApearDate >= startDate && ft.ApearDate <= endDate));
            
            var newAuthUsers = await _mainCtx.License
                .Where(l => l.User != null && l.ApearDate >= startDate && l.ApearDate <= endDate)
                .Select(l => l.User)
                .Distinct()             
                .CountAsync();

            var newCompanies = (from l in _mainCtx.License
                             join ca in _mainCtx.UserCompanyActivation
                                 on l.MacAddress equals ca.Mac
                             where l.ApearDate >= startDate && l.ApearDate <= endDate
                             group l by ca.CompanyName into g
                             select new
                             {
                                 CompanyName = g.Key,
                                 DeviceCount = g.Select(l => l.MacAddress).Distinct().Count()
                             }).ToList();

            var activatedKeys = await _mainCtx.LicenseKey
                .CountAsync(ak => ak.ActivatedDate >= startDate && ak.ActivatedDate <= endDate);

            var totalPayment = _mainCtx.Payments
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
                .Sum(p => (decimal?)p.Amount) ?? 0;

            var mainStats = new
            {
                NewDeviceCount = newDeviceCount,
                ActiveDevices = activeDevices,
                NewFreeTrials = newFreeTrials,
                NewAuthUsers = newAuthUsers,
                NewCompanies = newCompanies,
                ActivatedKeys = activatedKeys,
                TotalPayment = totalPayment
            };

            return Ok(mainStats);
        }

        private DateTime? GetDate(JsonElement json, string property)
            => json.TryGetProperty(property, out var val) ? DateTime.Parse(val.GetString()) : null;

    }
}
