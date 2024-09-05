using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        //Get All Sites
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSites()
        {
            var sites = await _siteService.GetAllSitesAsync();
            return Ok(sites);
        }

        // Get Site By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            var site = await _siteService.GetSiteByIdAsync(id);
            if (site == null)
            {
                return NotFound();
            }
            return Ok(site);
        }

        // Create site
        [HttpPost("create")]
        public async Task<IActionResult> AddSite([FromBody] SiteDTO siteDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            siteDTo.Id = Guid.NewGuid();
            siteDTo.CreatedDate = DateTime.Now;
            siteDTo.UpdatedBy = siteDTo.CreatedBy;
            siteDTo.UpdatedDate = siteDTo.UpdatedDate;
            await _siteService.AddSiteAsync(siteDTo);
            return CreatedAtAction(nameof(GetSiteById), new { id = siteDTo.Id }, siteDTo);
        }

        // Update site
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Updatesite([FromRoute]Guid id,[FromBody] SiteDTO siteDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != siteDTo.Id)
            {
                return BadRequest("SiteID Mismatch.");
            }
            var existingsite = await _siteService.GetSiteByIdAsync(siteDTo.Id);
            if (existingsite == null)
            {
                return NotFound();
            }
            existingsite.UpdatedDate = DateTime.Now; 
            await _siteService.UpdateSiteAsync(existingsite);
            return NoContent();
        }

        //Delete Site
        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var site = await _siteService.GetSiteByIdAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            await _siteService.DeleteSiteAsync(id);
            return NoContent();
        }
    }
}