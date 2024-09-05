using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual State State { get; set; }

    }
}
