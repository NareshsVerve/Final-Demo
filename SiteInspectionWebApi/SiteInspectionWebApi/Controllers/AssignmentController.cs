using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger _logger;
        public AssignmentController(IAssignmentService assignmentService,ILogger<AssignmentController> logger)
        {
            _assignmentService = assignmentService;
            _logger = logger;
        }

        // Get All Assignment
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
           
            return Ok(assignments);
        }

        // Get Assignment By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAssignmentById(Guid id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }   
            return Ok(assignment);
        }

        // Get Assignment by InspectorId
        [HttpGet("assignment-by-inspectorid/{Id:guid}")]
        public async Task<IActionResult> GetAssignmentsByInspectorId(Guid id)
        {
            var assignments = await _assignmentService.GetAssignmentsByInspectorAsync(id);
            if (assignments == null)
            {
                return NotFound();
            }
            return Ok(assignments);
        }

        // Get Assignment by Date
        [HttpGet("assignment-by-Inspection-date/{date:datetime}")]
        public async Task<IActionResult> GetAssignmentsByDate(DateTime date)
        {
            var assignments = await _assignmentService.GetAllAssignmentsByInspectionDateAsync(date);
            if (assignments == null)
            {
                return NotFound();
            }
            return Ok(assignments);
        }

        // Create site
        [HttpPost("create")]
        public async Task<IActionResult> AddAssignment([FromBody] AssignmentDTO assignmentDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            assignmentDTo.Id = Guid.NewGuid();
            assignmentDTo.CreatedDate = DateTime.Now;
            assignmentDTo.UpdatedBy = assignmentDTo.CreatedBy;
            assignmentDTo.UpdatedDate = assignmentDTo.CreatedDate;

            await _assignmentService.AddAssignmentAsync(assignmentDTo);

            return CreatedAtAction(nameof(GetAssignmentById), new { id = assignmentDTo.Id }, assignmentDTo);
        }

        // Update site
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateAssignment([FromRoute] Guid id, [FromBody] AssignmentDTO assignmentDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != assignmentDTo.Id)
            {
                return BadRequest("AssignmentId Mismatch.");
            }
            var existingAssignment = await _assignmentService.GetAssignmentByIdAsync(assignmentDTo.Id);
            if (existingAssignment.Id != assignmentDTo.Id)
            {
                return BadRequest("AssignmentId Mismatch.");
            }
            if (existingAssignment == null)
            {
                return NotFound();
            }
            _assignmentService.UpdateAssignment(assignmentDTo);

            return NoContent();
        }

        //Delete Site
        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            await _assignmentService.DeleteAssignmentAsync(id);
            return NoContent();
        }
    }
}
