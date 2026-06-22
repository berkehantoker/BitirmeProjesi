using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitirme.Services.Abstract
{
    public interface IAiService
    {
        Task<string> GenerateDetailedReportAsync(double scoreA, double scoreB, double scoreC, List<string> detailedResponses);
    }
}