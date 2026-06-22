using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitirme.Core.DTOs
{
    public class TestAnalysisResultDto
    {
        public string Message { get; set; }
        public List<DimensionScoreDto> Analysis { get; set; }
        public string AiReport { get; set; }
    }
}
