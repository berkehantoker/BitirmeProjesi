using Bitirme.Core.DTOs;
using Bitirme.Repository.Abstract;
using Bitirme.Services.Abstract;
using System.Threading.Tasks;

namespace Bitirme.Business.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminManager(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<string> GetExportDataCsvAsync()
        {
            return await _adminRepository.GetExportDataCsvAsync();
        }

        public async Task<AdminStatsDto> GetSystemStatisticsAsync()
        {
            return await _adminRepository.GetGeneralStatisticsAsync();
        }
    }
}