using Microsoft.AspNetCore.Http.HttpResults;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Repository;


namespace SiteInspectionWebApi.Service
{
    public class SiteService : ISiteService
    {
        private ISiteRepository _siteRepository;
        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
        public async Task<SiteDTO> GetSiteByIdAsync(Guid id)
        {
            var site = await _siteRepository.GetSiteByIdAsync(id);
            if (site == null)
            {
                throw new Exception("Site not found");
            }
            var siteDto = SiteDTO.Mapping(site);
            return siteDto;

        }
        public async Task<IEnumerable<SiteDTO>> GetAllSitesAsync()
        {
            var sites = await _siteRepository.GetAllSitesAsync();
            if (sites == null)
            {
                throw new Exception("Site not found");
            }
            var siteDtos = SiteDTO.Mapping(sites);
            return siteDtos;
        }
        public async Task AddSiteAsync(SiteDTO siteDto)
        {
            siteDto.Id = Guid.NewGuid();
            siteDto.CreatedDate = DateTime.Now;
            siteDto.UpdatedBy = siteDto.CreatedBy;
            siteDto.UpdatedDate = siteDto.CreatedDate;

            var site = SiteDTO.Mapping(siteDto);
            await _siteRepository.AddSiteAsync(site);
        }
        public async Task UpdateSiteAsync(SiteDTO siteDto)
        {
            var existingsite = await _siteRepository.GetSiteByIdAsync(siteDto.Id);
            if(existingsite == null)
            {
                throw new Exception("site Not Found");
            }
            existingsite.UserId = siteDto.UserId;
            existingsite.Name = siteDto.Name;
            existingsite.Description = siteDto.Description;
            existingsite.CountryId = siteDto.CountryId;
            existingsite.StateId = siteDto.StateId;
            existingsite.CityId = siteDto.CityId;
            existingsite.UpdatedBy = siteDto.UpdatedBy;
            existingsite.UpdatedDate = DateTime.Now;

            await _siteRepository.UpdateSiteAsync(existingsite);
        }
        public async Task DeleteSiteAsync(Guid id)
        {
            await _siteRepository.DeleteSiteAsync(id);
        }
    }
}
