using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class LoggerEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public string? Source { get; set; }
    }
}
