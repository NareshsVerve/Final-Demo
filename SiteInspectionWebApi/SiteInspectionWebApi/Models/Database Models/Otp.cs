using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class Otp
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Code { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
