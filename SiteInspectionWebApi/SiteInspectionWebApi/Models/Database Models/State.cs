using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SiteInspectionWebApi.Models.Database_Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [JsonIgnore]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        [JsonIgnore]
        public virtual ICollection<City> Cities { get; set; }
       
    }
}
