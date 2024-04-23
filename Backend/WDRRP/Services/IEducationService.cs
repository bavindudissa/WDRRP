using WDRRP.Dtos;

namespace WDRRP.Services;

public interface IEducationService
{
    Task<IEnumerable<EducationDto>> GetEducations(int userId);
    Task<EducationDto> GetEducation(int educationId);
    Task<EducationDto> AddEducation(EducationDto education);
    Task<EducationDto> UpdateEducation(EducationDto education);
    Task<bool> DeleteEducation(EducationDto education);
}
