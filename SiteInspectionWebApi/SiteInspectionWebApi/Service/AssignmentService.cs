using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Service
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<AssignmentDTO> GetAssignmentByIdAsync(Guid id)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            var assignmentDto = AssignmentDTO.Mapping(assignment);
            return assignmentDto;
        }
        public async Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsAsync()
        {
           var assignments = await _assignmentRepository.GetAllAssignmentsAsync();
            var assignmentDtos = AssignmentDTO.Mapping(assignments);
            return assignmentDtos;
        }
        public async Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsByInspectionDateAsync(DateTime date)
        {
            var assignments = await _assignmentRepository.GetAllAssignmentsByInspectionDateAsync(date);
            var assignmentDtos = AssignmentDTO.Mapping(assignments);
            return assignmentDtos;
        }
        public async Task<IEnumerable<AssignmentDTO>> GetAssignmentsByInspectorAsync(Guid inspectorId)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByInspectorAsync(inspectorId);
            var assignmentDtos = AssignmentDTO.Mapping(assignments);
            return assignmentDtos;
        }
        public async Task AddAssignmentAsync(AssignmentDTO assignmentDto)
        {
            var assignment = AssignmentDTO.Mapping(assignmentDto);
            await _assignmentRepository.AddAssignmentAsync(assignment);
        }
        public async Task UpdateAssignment(AssignmentDTO assignmentDto)
        {
            var assignment = AssignmentDTO.Mapping(assignmentDto);
             _assignmentRepository.UpdateAssignmentAsync(assignment);
        }
        public async Task DeleteAssignmentAsync(Guid id)
        {
            await _assignmentRepository.DeleteAssignmentAsync(id);
        }
    }
}
