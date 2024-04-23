using WDRRP.Dtos;
using WDRRP.Models;

namespace WDRRP.Services;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetSkills(int userId);
    Task<SkillDto> GetSkill(int skillId);
    Task<SkillDto> AddSkill(SkillDto skill);
    Task<SkillDto> UpdateSkill(SkillDto skill);
    Task<bool> DeleteSkill(SkillDto skill);
    //Task<IEnumerable<SkillDto>> SearchSkills(string userName);
}
