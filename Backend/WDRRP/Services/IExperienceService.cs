using WDRRP.Dtos;

namespace WDRRP.Services;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceDto>> GetExperiences(int userId);
    Task<ExperienceDto> GetExperience(int experienceId);
    Task<ExperienceDto> AddExperience(ExperienceDto experience);
    Task<ExperienceDto> UpdateExperience(ExperienceDto experience);
    Task<bool> DeleteExperience(ExperienceDto experience);
}
