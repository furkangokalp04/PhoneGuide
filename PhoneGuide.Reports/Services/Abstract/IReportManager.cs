using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneGuide.Reports.Services.Abstract
{
    public interface IReportManager
    {
        Task<DataResult<List<ReportDto>>> GetAllAsync();
        Task<DataResult<ReportDto>> GetByIdAsync(string id);
        Task<DataResult<ReportDto>> AddAsync(ReportDto model);
        Task<Result> UpdateAsync(ReportDto model);
    }
}
