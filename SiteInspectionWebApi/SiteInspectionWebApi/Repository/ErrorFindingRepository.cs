using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Repository
{
    public class ErrorFindingRepository: IErrorFindingsRepository
    {
        private ApplicationDbContext _context;
        public ErrorFindingRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<ErrorFinding> GetFindingErrorByIdAsync(Guid id)
        {
            return await _context.ErrorFindings.FindAsync(id);
        }
        public async Task<IEnumerable<ErrorFinding>> GetFindingErrorByAssignmentIdAsync(Guid assignmentId)
        {
            return await _context.ErrorFindings.Where(e=> e.AssignmentId == assignmentId).ToListAsync();
        }
        public async Task<IEnumerable<ErrorFinding>> GetAllFindingErrorsAsync()
        {
            return await _context.ErrorFindings.ToListAsync();
        }
        public async Task AddFindingErrorAsync(ErrorFinding errorFinding)
        {
            await _context.ErrorFindings.AddAsync(errorFinding);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateFindingErrorAsync(ErrorFinding errorFinding)
        {
             _context.ErrorFindings.Update(errorFinding);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFindingErrorAsync(Guid id)
        {
            var findingError = await _context.ErrorFindings.FindAsync(id);
            if (findingError != null)
            {
                _context.ErrorFindings.Remove(findingError);
                await _context.SaveChangesAsync();
            }
        }
    }
}
