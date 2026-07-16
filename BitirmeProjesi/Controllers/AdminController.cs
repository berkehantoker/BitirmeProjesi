using Bitirme.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BitirmeProjesi.Controllers
{
    
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _adminService.GetSystemStatisticsAsync();
            return Ok(stats);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            // Csv datasını servisten çekiyoruz
            var csvData = await _adminService.GetExportDataCsvAsync();
            var bytes = System.Text.Encoding.UTF8.GetBytes(csvData);

            // Tarayıcıya "Bunu bir Excel/CSV dosyası olarak indir" emri veriyoruz
            return File(bytes, "text/csv", $"CasDataExport_{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}