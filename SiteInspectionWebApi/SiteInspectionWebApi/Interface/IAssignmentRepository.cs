using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface IAssignmentRepository
    {
        Task<Assignment> GetAssignmentByIdAsync(Guid id);
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<IEnumerable<Assignment>> GetAllAssignmentsByInspectionDateAsync(DateTime date);
        Task<IEnumerable<Assignment>> GetAssignmentsByInspectorAsync(Guid inspectorId);
        Task AddAssignmentAsync(Assignment assignment);
        Task UpdateAssignmentAsync(Assignment assignment);
    }
}
