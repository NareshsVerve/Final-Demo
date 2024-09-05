using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Service
{
    public class ErrorFindingService:IErrorrFindingService
    {
        private readonly IErrorFindingsRepository _errorFindingsRepository;
        public ErrorFindingService(IErrorFindingsRepository errorFindingsRepository)
        {
            _errorFindingsRepository = errorFindingsRepository;
        }
        public async Task<ErrorFindingDTO> GetFindingErrorByIdAsync(Guid id)
        {
            var findingError = await _errorFindingsRepository.GetFindingErrorByIdAsync(id);
            if (findingError == null)
            {
                return null;
            }
            var findingErrorDto = ErrorFindingDTO.Mapping(findingError);
            return findingErrorDto;
        }
        public async Task<IEnumerable<ErrorFindingDTO>> GetFindingErrorByAssignmentIdAsync(Guid assignmentId)
        {
            var findingErrors = await _errorFindingsRepository.GetFindingErrorByAssignmentIdAsync(assignmentId);
            if (findingErrors == null)
            {
                return null;
            }
            var findingErrorDtos = ErrorFindingDTO.Mapping(findingErrors);
            return findingErrorDtos;
        }
        public async Task<IEnumerable<ErrorFindingDTO>> GetAllFindingErrorsAsync()
        {
           var findingErrors = await _errorFindingsRepository.GetAllFindingErrorsAsync();
            var findingErrorDtos = ErrorFindingDTO.Mapping(findingErrors);
            return findingErrorDtos;
        }
        public async Task AddFindingErrorAsync(ErrorFindingDTO errorFindingDto)
        {
            var errorFinding = ErrorFindingDTO.Mapping(errorFindingDto);
           await _errorFindingsRepository.AddFindingErrorAsync(errorFinding);
        }
        public async Task UpdateFindingErrorAsync(ErrorFindingDTO errorFindingDto)
        {
            var existingErrorFinding = await _errorFindingsRepository.GetFindingErrorByIdAsync(errorFindingDto.Id);
            existingErrorFinding.Description = errorFindingDto.Description;
            existingErrorFinding.Image = errorFindingDto.Image;
            existingErrorFinding.ImageUploadByClient = errorFindingDto.ImageUploadByClient;
            existingErrorFinding.ResolutionStatus = (int)errorFindingDto.ResolutionStatus;
            existingErrorFinding.Criticality = (int)errorFindingDto.Criticality;
            existingErrorFinding.UpdatedDate = DateTime.Now;
            existingErrorFinding.UpdatedBy = errorFindingDto.UpdatedBy;

            await _errorFindingsRepository.UpdateFindingErrorAsync(existingErrorFinding);
        }
        public async Task DeleteFindingErrorAsync(Guid id)
        {
            await _errorFindingsRepository.DeleteFindingErrorAsync(id);
        }
    }
}
