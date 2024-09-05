using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IErrorrFindingService
    {
        Task<ErrorFindingDTO> GetFindingErrorByIdAsync(Guid id);
        Task<IEnumerable<ErrorFindingDTO>> GetFindingErrorByAssignmentIdAsync(Guid assignmentId);
        Task<IEnumerable<ErrorFindingDTO>> GetAllFindingErrorsAsync();
        Task AddFindingErrorAsync(ErrorFindingDTO errorFindingDto);
        Task UpdateFindingErrorAsync(ErrorFindingDTO errorFindingDto);
        Task DeleteFindingErrorAsync(Guid id);
    }
}
