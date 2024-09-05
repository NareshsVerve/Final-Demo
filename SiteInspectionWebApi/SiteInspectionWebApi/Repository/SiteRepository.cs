using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Repository
{
    public class SiteRepository : ISiteRepository
    {
        private readonly ApplicationDbContext _context;
        public SiteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Site> GetSiteByIdAsync(Guid id)
        {
            return await _context.Sites
                                 .Include(s => s.Country)
                                 .Include(s => s.State)
                                 .Include(s => s.City)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Site>> GetAllSitesAsync()
        {
            return await _context.Sites
                                 .Include(s => s.Country)
                                 .Include(s => s.State)
                                 .Include(s => s.City)
                                 .ToListAsync();
        }

        public async Task AddSiteAsync(Site site)
        {
            await _context.Sites.AddAsync(site);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateSiteAsync(Site site)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSiteAsync(Guid id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site != null)
            {
                _context.Sites.Remove(site);
                await _context.SaveChangesAsync();
            }
        }
    }
}
