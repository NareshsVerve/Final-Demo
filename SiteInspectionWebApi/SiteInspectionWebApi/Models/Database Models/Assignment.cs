using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiteInspectionWebApi.Models.Enums;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class Assignment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public Guid UserId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Notes { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }

        // Navigation properties 
        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
/*        public virtual ICollection<ErrorFinding> ErrorFindings { get; set; }*/

    }
}


