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
            var siteDto = SiteDTO.Mapping(site);
            return siteDto;

        }
        public async Task<IEnumerable<SiteDTO>> GetAllSitesAsync()
        {
            var sites = await _siteRepository.GetAllSitesAsync();
            var siteDtos = SiteDTO.Mapping(sites);
            return siteDtos;
        }
        public async Task AddSiteAsync(SiteDTO siteDto)
        {
            var site = SiteDTO.Mapping(siteDto);
            await _siteRepository.AddSiteAsync(site);
        }
        public async Task UpdateSiteAsync(SiteDTO siteDto)
        {
            var site = SiteDTO.Mapping(siteDto);
            await _siteRepository.UpdateSiteAsync(site);
        }
        public async Task DeleteSiteAsync(Guid id)
        {
            await _siteRepository.DeleteSiteAsync(id);
        }
    }
}
