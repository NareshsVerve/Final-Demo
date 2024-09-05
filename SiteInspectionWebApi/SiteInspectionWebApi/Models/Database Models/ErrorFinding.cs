using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class ErrorFinding
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public string Description { get; set; } 
        public string? Image { get; set; } 
        public string? ImageUploadByClient { get; set; } 
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Criticality { get; set; }
        public int ResolutionStatus { get; set; }

        // Navigation Properties
        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment    { get; set; }
    }
}
