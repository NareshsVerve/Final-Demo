using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.Database_Models
{
   
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? ProfileImage { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Token { get; set; }
        public bool IsActive { get; set; }
        public int Role { get; set; }

        // Navigation properties
        public virtual ICollection<Site> Sites { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }

}
