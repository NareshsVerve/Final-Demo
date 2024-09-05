using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.DTO
{
    public class AssignmentDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid SiteId { get; set; }
        [Required]
        public Guid InspectorId { get; set; }
        [Required]
        public DateTime InspectionDate { get; set; }
        [Required]
        public string? Notes { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; } 
        public bool IsActive { get; set; } = true;
        public InspectionStatus Status { get; set; }
        public static Assignment Mapping(AssignmentDTO assignmentDto)
        {
            return new Assignment()
            {
                Id = assignmentDto.Id,
                SiteId = assignmentDto.SiteId,
                UserId = assignmentDto.InspectorId,
                InspectionDate = assignmentDto.InspectionDate,
                Notes = assignmentDto.Notes,
                CreatedBy = assignmentDto.CreatedBy,
                CreatedDate = assignmentDto.CreatedDate,
                UpdatedBy = assignmentDto.UpdatedBy,
                UpdatedDate = assignmentDto.UpdatedDate,
                Status = (int)assignmentDto.Status,
                IsActive = assignmentDto.IsActive
            };
        }

        public static IEnumerable<Assignment> Mapping(IEnumerable<AssignmentDTO> assignmentDtos)
        {
            var assignments = new List<Assignment>();
            foreach (var assignmentDto in assignmentDtos)
            {
                assignments.Add(Mapping(assignmentDto));
            }
            return assignments;
        }

        public static AssignmentDTO Mapping(Assignment assignment)
        {
            return new AssignmentDTO()
            {
                Id = assignment.Id,
                SiteId = assignment.SiteId,
                InspectorId = assignment.UserId,
                InspectionDate = assignment.InspectionDate,
                Notes = assignment.Notes,
                CreatedBy = assignment.CreatedBy,
                CreatedDate = assignment.CreatedDate,
                UpdatedBy = assignment.UpdatedBy,
                UpdatedDate = assignment.UpdatedDate,
                Status = (InspectionStatus)assignment.Status,
                IsActive = assignment.IsActive
            };
        }

        public static IEnumerable<AssignmentDTO> Mapping(IEnumerable<Assignment> assignments)
        {
            var assignmentDtos = new List<AssignmentDTO>();
            foreach (var assignment in assignments)
            {
                assignmentDtos.Add(Mapping(assignment));

            }
            return assignmentDtos;
        }
    }
}
