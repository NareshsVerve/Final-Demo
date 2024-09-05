using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public  virtual ICollection<State> States { get; set; }
    }
}
