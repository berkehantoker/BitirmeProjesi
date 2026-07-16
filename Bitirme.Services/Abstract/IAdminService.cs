using Bitirme.Core.DTOs;
using System.Threading.Tasks;

namespace Bitirme.Services.Abstract
{
    public interface IAdminService
    {
        Task<AdminStatsDto> GetSystemStatisticsAsync();

        Task<string> GetExportDataCsvAsync();
    }
}