using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

    }
}
