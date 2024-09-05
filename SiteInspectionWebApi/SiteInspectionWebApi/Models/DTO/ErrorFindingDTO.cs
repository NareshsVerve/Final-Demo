using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace SiteInspectionWebApi.Models.DTO
{
    public class ErrorFindingDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid AssignmentId { get; set; }
        public string Description { get; set; } 
        public string? Image { get; set; }
      
        public string? ImageUploadByClient { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Criticality Criticality { get; set; }
        public ResolutionStatus ResolutionStatus { get; set; }

        public static ErrorFinding Mapping(ErrorFindingDTO errorFindingDto)
        {
            return new ErrorFinding()
            {
                Id = errorFindingDto.Id,
                AssignmentId = errorFindingDto.AssignmentId,
                Description = errorFindingDto.Description,
                Image = errorFindingDto.Image,
                ImageUploadByClient = errorFindingDto.ImageUploadByClient,
                CreatedBy = errorFindingDto.CreatedBy,
                CreatedDate = errorFindingDto.CreatedDate,
                UpdatedBy = errorFindingDto.UpdatedBy,
                UpdatedDate = errorFindingDto.UpdatedDate,
                Criticality = (int)errorFindingDto.Criticality,
                ResolutionStatus = (int)errorFindingDto.ResolutionStatus
            };
        }

        public static IEnumerable<ErrorFinding> Mapping(IEnumerable<ErrorFindingDTO> errorFindingDtos)
        {
            var errorFindings = new List<ErrorFinding>();
            foreach (var errorFindingDto in errorFindingDtos)
            {
                errorFindings.Add(Mapping(errorFindingDto));
            }
            return errorFindings;
        }

        public static ErrorFindingDTO Mapping(ErrorFinding errorFinding)
        {
            return new ErrorFindingDTO()
            {
                Id = errorFinding.Id,
                AssignmentId = errorFinding.AssignmentId,
                Description = errorFinding.Description,
                Image = errorFinding.Image,
                ImageUploadByClient = errorFinding.ImageUploadByClient,
                CreatedBy = errorFinding.CreatedBy,
                CreatedDate = errorFinding.CreatedDate,
                UpdatedBy = errorFinding.UpdatedBy,
                UpdatedDate = errorFinding.UpdatedDate,
                Criticality = (Criticality)errorFinding.Criticality,
                ResolutionStatus = (ResolutionStatus)errorFinding.ResolutionStatus
            };
        }

        public static IEnumerable<ErrorFindingDTO> Mapping(IEnumerable<ErrorFinding> errorFindings)
        {
            var errorFindingDtos = new List<ErrorFindingDTO>();
            foreach (var errorFinding in errorFindings)
            {
                errorFindingDtos.Add(Mapping(errorFinding));

            }
            return errorFindingDtos;
        }
    }
}
