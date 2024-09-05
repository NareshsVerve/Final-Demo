using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorFindingController : ControllerBase
    {
        private readonly IErrorrFindingService _errorrFindingService;
        private readonly ILogger<ErrorFindingController> _logger;

        public ErrorFindingController(IErrorrFindingService errorrFindingService, ILogger<ErrorFindingController> logger)
        {
            _errorrFindingService = errorrFindingService;
            _logger = logger; 
        }

        // Get All Errors in different Assignments
        [HttpGet("all")]
        public async Task<IActionResult> GetAllErrors()
        {
            try
            {
                var errors = await _errorrFindingService.GetAllFindingErrorsAsync();
                return Ok(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all errors.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get Error in Assignment By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetErrorById(Guid id)
        {
            try
            {
                var error = await _errorrFindingService.GetFindingErrorByIdAsync(id);
                if (error == null)
                {
                    return NotFound();
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the error with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        // Get Errors in a particular Assignment 
        [HttpGet("by-assignment/{id:guid}")]
        public async Task<IActionResult> GetErrorBySIteId(Guid id)
        {
            try
            {
                var error = await _errorrFindingService.GetFindingErrorByAssignmentIdAsync(id);
                if (error == null)
                {
                    return NotFound();
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the error with Assignment ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        // Create Error found in Assignment
        [HttpPost("create")]
        public async Task<IActionResult> AddErrorFound([FromBody] ErrorFindingDTO errorFindingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _errorrFindingService.AddFindingErrorAsync(errorFindingDto);
                return CreatedAtAction(nameof(GetErrorById), new { id = errorFindingDto.Id }, errorFindingDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new Error Found.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Update Finding Errors
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateSite([FromRoute] Guid id, [FromBody] ErrorFindingDTO errorFindingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != errorFindingDto.Id)
                {
                    return BadRequest("SiteID Mismatch.");
                }
                await _errorrFindingService.UpdateFindingErrorAsync(errorFindingDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the finidnf error with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Delete Finding Error
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var site = await _errorrFindingService.GetFindingErrorByIdAsync(id);
                if (site == null)
                {
                    return NotFound();
                }

                await _errorrFindingService.DeleteFindingErrorAsync(id);
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
