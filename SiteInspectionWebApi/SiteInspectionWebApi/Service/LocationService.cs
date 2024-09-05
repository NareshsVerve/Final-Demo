using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Service
{
    public class LocationService: ILocationServices
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _locationRepository.GetAllCountries();
        }
        public async Task<IEnumerable<State>> GetAllStates(int CountryId)
        {
            return await _locationRepository.GetAllStates(CountryId);
        }
        public async Task<IEnumerable<City>> GetAllCities(int StateId)
        {
            return await _locationRepository.GetAllCities(StateId); 
        }
    }
}
