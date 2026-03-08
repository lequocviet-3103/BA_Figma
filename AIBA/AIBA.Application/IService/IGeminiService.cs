using AIBA.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Application.IService
{
    public interface IGeminiService
    {
        Task<BAResultDto> GenerateAnalysisAsync(string idea);
    }
}
