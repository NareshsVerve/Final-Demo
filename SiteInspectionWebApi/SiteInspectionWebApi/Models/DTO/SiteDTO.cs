using SiteInspectionWebApi.Models.Database_Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInspectionWebApi.Models.DTO
{
    public class SiteDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = null!;
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        public string? Description { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } 
        [Required]
        public Guid UpdatedBy { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        public virtual Country? Country { get; set; }
        public virtual State? State { get; set; }
        public virtual City? City { get; set; }

        public static Site Mapping(SiteDTO siteDto)
        {
            return new Site()
            {
                Id = siteDto.Id,
                UserId = siteDto.UserId,
                Name = siteDto.Name,
                Address = siteDto.Address,
                CountryId = siteDto.CountryId,
                StateId = siteDto.StateId,
                CityId = siteDto.CityId,
                Description = siteDto.Description,
                CreatedBy = siteDto.CreatedBy,
                CreatedDate = siteDto.CreatedDate,
                UpdatedBy = siteDto.UpdatedBy,
                UpdatedDate = siteDto.UpdatedDate,
                
            };
        }

        public static IEnumerable<Site> Mapping(IEnumerable<SiteDTO> siteDtos)
        {
            var sites = new List<Site>();
            foreach (var siteDto in siteDtos)
            {
                sites.Add(Mapping(siteDto));
            }
            return sites;
        }

        public static SiteDTO Mapping(Site site)
        {
            return new SiteDTO()
            {
                Id = site.Id,
                UserId = site.UserId,
                Name = site.Name,
                Address = site.Address,
                CountryId = site.CountryId,
                StateId = site.StateId,
                CityId = site.CityId,
                Description = site.Description,
                CreatedBy = site.CreatedBy,
                CreatedDate = site.CreatedDate,
                UpdatedBy = site.UpdatedBy,
                UpdatedDate = site.UpdatedDate,
                Country = site.Country,
                State = site.State,
                City = site.City
            };
        }

        public static IEnumerable<SiteDTO> Mapping(IEnumerable<Site> sites)
        {
            var siteDtos = new List<SiteDTO>();
            foreach (var site in sites)
            {
                siteDtos.Add(Mapping(site));

            }
            return siteDtos;
        }
    }
}
