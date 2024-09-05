using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using System.Data.SqlTypes;

namespace SiteInspectionWebApi.Service
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ILogger<AssignmentService> _logger;
        public AssignmentService(IAssignmentRepository assignmentRepository, ILogger<AssignmentService> logger)
        {
            _assignmentRepository = assignmentRepository;
            _logger = logger;
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
            var existingAssignment = await _assignmentRepository.GetAssignmentByIdAsync(assignmentDto.Id);
            existingAssignment.Notes = assignmentDto.Notes;
            existingAssignment.SiteId = assignmentDto.SiteId;
            existingAssignment.Status = (int)assignmentDto.Status;
            existingAssignment.UpdatedDate = DateTime.Now;
            existingAssignment.UpdatedBy = assignmentDto.UpdatedBy;

            await _assignmentRepository.UpdateAssignmentAsync(existingAssignment);
        }
        public async Task DeleteAssignmentAsync(Guid id)
        {
            var existingAssignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            existingAssignment.IsActive = false;
            await _assignmentRepository.UpdateAssignmentAsync(existingAssignment);
        }
    }
}
