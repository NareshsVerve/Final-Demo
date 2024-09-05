using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Service;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly ILogger<SiteController> _logger; 

        public SiteController(ISiteService siteService, ILogger<SiteController> logger)
        {
            _siteService = siteService;
            _logger = logger; // Initialize logger
        }

        // Get All Sites
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSites()
        {
            try
            {
                var sites = await _siteService.GetAllSitesAsync();
                return Ok(sites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all sites.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get Site By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            try
            {
                var site = await _siteService.GetSiteByIdAsync(id);
                if (site == null)
                {
                    return NotFound();
                }
                return Ok(site);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the site with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Create Site
        [HttpPost("create")]
        public async Task<IActionResult> AddSite([FromBody] SiteDTO siteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _siteService.AddSiteAsync(siteDto);
                return CreatedAtAction(nameof(GetSiteById), new { id = siteDto.Id }, siteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new site.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Update Site
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateSite([FromRoute] Guid id, [FromBody] SiteDTO siteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != siteDto.Id)
                {
                    return BadRequest("SiteID Mismatch.");
                }
                await _siteService.UpdateSiteAsync(siteDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the site with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Delete Site
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var site = await _siteService.GetSiteByIdAsync(id);
                if (site == null)
                {
                    return NotFound();
                }

                await _siteService.DeleteSiteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the site with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
