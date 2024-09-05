using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface IErrorFindingsRepository
    {
        Task<ErrorFinding> GetFindingErrorByIdAsync(Guid id);
        Task<IEnumerable<ErrorFinding>> GetFindingErrorByAssignmentIdAsync(Guid assignmentId);
        Task<IEnumerable<ErrorFinding>> GetAllFindingErrorsAsync();
        Task AddFindingErrorAsync(ErrorFinding errorFinding);
        Task UpdateFindingErrorAsync(ErrorFinding errorFinding);
        Task DeleteFindingErrorAsync(Guid id);
    }
}
