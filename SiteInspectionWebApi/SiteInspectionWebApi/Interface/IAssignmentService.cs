using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IAssignmentService
    {
        Task<AssignmentDTO> GetAssignmentByIdAsync(Guid id);
        Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsAsync();
        Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsByInspectionDateAsync(DateTime date);
        Task<IEnumerable<AssignmentDTO>> GetAssignmentsByInspectorAsync(Guid inspectorId);
        Task AddAssignmentAsync(AssignmentDTO assignmentDto);
        Task UpdateAssignment(AssignmentDTO assignmentDto);
        Task DeleteAssignmentAsync(Guid id);
    }
}
