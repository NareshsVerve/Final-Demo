using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface ISiteService
    {
        Task<SiteDTO> GetSiteByIdAsync(Guid id);
        Task<IEnumerable<SiteDTO>> GetAllSitesAsync();
        Task AddSiteAsync(SiteDTO siteDto);
        Task UpdateSiteAsync(SiteDTO siteDto);
        Task DeleteSiteAsync(Guid id);
    }
}
