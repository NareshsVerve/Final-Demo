using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<State>> GetAllStates(int CountryId);
        Task<IEnumerable<City>> GetAllCities(int StateId);
    }
}
