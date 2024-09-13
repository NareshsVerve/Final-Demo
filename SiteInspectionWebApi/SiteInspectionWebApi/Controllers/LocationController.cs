using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteInspectionWebApi.Interface;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationServices _locationService;
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILocationServices locationService,ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }
        
        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            try{

                var countries = await _locationService.GetAllCountries();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the Countries.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStates(int countryId)
        {
            try
            {

                var states = await _locationService.GetAllStates(countryId);
                return Ok(states);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the states.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("cities")]
        public async Task<IActionResult> Getcities(int stateId)
        {
            try
            {

                var cities = await _locationService.GetAllCities(stateId);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the cities.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

    }
}
