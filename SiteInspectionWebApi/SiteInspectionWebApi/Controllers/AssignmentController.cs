using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add this for logging
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Service;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ILogger<AssignmentController> _logger; 

        public AssignmentController(IAssignmentService assignmentService, ILogger<AssignmentController> logger)
        {
            _assignmentService = assignmentService;
            _logger = logger; // Initialize logger
        }

        // Get All Assignments
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAssignments()
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignmentsAsync();
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all assignments.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get Assignment By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAssignmentById(Guid id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound();
                }
                return Ok(assignment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the assignment with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get Assignments by InspectorId
        [HttpGet("assignment-by-inspectorid/{id:guid}")]
        public async Task<IActionResult> GetAssignmentsByInspectorId(Guid id)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByInspectorAsync(id);
                if (assignments == null || !assignments.Any()) // Check for null or empty
                {
                    return NotFound();
                }
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving assignments for inspector ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get Assignments by Date
        [HttpGet("assignment-by-inspection-date/{date:datetime}")]
        public async Task<IActionResult> GetAssignmentsByDate(DateTime date)
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignmentsByInspectionDateAsync(date);
                if (assignments == null || !assignments.Any()) // Check for null or empty
                {
                    return NotFound();
                }
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving assignments for date: {date}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Create Assignment
        [HttpPost("create")]
        public async Task<IActionResult> AddAssignment([FromBody] AssignmentDTO assignmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                assignmentDto.Id = Guid.NewGuid();
                assignmentDto.CreatedDate = DateTime.Now;
                assignmentDto.UpdatedBy = assignmentDto.CreatedBy;
                assignmentDto.UpdatedDate = assignmentDto.CreatedDate;

                await _assignmentService.AddAssignmentAsync(assignmentDto);
                return CreatedAtAction(nameof(GetAssignmentById), new { id = assignmentDto.Id }, assignmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new assignment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Update Assignment
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateAssignment([FromRoute] Guid id, [FromBody] AssignmentDTO assignmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != assignmentDto.Id)
                {
                    return BadRequest("AssignmentId Mismatch.");
                }

                await _assignmentService.UpdateAssignment(assignmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the assignment with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Delete Assignment
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound();
                }

                await _assignmentService.DeleteAssignmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the assignment with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
