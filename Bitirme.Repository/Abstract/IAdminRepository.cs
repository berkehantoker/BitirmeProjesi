using Bitirme.Core.DTOs;
using System.Threading.Tasks;

namespace Bitirme.Repository.Abstract
{
    public interface IAdminRepository
    {
        Task<AdminStatsDto> GetGeneralStatisticsAsync();
    }
}