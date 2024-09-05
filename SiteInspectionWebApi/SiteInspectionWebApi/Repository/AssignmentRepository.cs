using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiteInspectionWebApi.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await _context.Assignments.ToListAsync();
        }
        public async Task<Assignment> GetAssignmentByIdAsync(Guid id)
        {
            return await _context.Assignments.FindAsync(id);
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsByInspectionDateAsync(DateTime date)
        {
            return await _context.Assignments.Where(a => a.InspectionDate.Date == date.Date).ToListAsync();
        }
        public async Task<IEnumerable<Assignment>> GetAssignmentsByInspectorAsync(Guid inspectorId)
        {
            return await _context.Assignments.Where(a => a.UserId == inspectorId).ToListAsync();
        }
        public async Task AddAssignmentAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            _context.SaveChanges();
        }
        public async Task DeleteAssignmentAsync(Guid id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
