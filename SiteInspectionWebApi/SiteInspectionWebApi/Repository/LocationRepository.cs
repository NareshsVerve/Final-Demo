using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Repository
{
    public class LocationRepository: ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _context.Countries.ToListAsync();
        }
        public async Task<IEnumerable<State>> GetAllStates(int countryId)
        {
            return await _context.States.Where(s => s.CountryId == countryId).ToListAsync();
        }
        public async Task<IEnumerable<City>> GetAllCities(int stateId)
        {
            return await _context.Cities.Where(c => c.StateId == stateId).ToListAsync();
        }
    }
}
