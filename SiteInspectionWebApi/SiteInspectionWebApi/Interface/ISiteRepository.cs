using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface ISiteRepository
    {
        Task<Site> GetSiteByIdAsync(Guid id);
        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task AddSiteAsync(Site Site);
        Task UpdateSiteAsync(Site Site);
        Task DeleteSiteAsync(Guid id);
    }
}
