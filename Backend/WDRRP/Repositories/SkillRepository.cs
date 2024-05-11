using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class SkillRepository : ISkillService
{
    private readonly WdrrpContext _dbContext;
    public SkillRepository(WdrrpContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<SkillDto> AddSkill(SkillDto skill)
    {
        var resultCheck = await _dbContext.Skills
            .FirstOrDefaultAsync(e => e.Skill1 == skill.Skill1 && e.IsActive == true);

        if (resultCheck != null)
        {
            throw new InvalidOperationException("Skill is alredy used.");
        }    

        var newSkill = new Skill{
            Skill1 = skill.Skill1,
            UserId = skill.UserId,
            CreatedBy = 0,
            CreatedAt = DateTime.Now,
            UpdatedBy = null,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.Skills.AddAsync(newSkill);
        await _dbContext.SaveChangesAsync();
        
        var response = new SkillDto{
            Id = newSkill.Id,
            Skill1 = newSkill.Skill1,
            UserId = newSkill.UserId,
            IsActive = newSkill.IsActive
        };

        return response;
    }

    public async Task<bool> DeleteSkill(SkillDto skill)
    {
        var result = await _dbContext.Skills
            .FirstOrDefaultAsync(e => e.Id == skill.Id);

        if (result == null)
        {
            throw new InvalidOperationException("Skill not found.");
        }

        result.UpdatedBy = 0; //This will replace the jwt token claim userId
        result.UpdatedAt = DateTime.Now;
        result.IsActive = false;

        _dbContext.Skills.Update(result);

        await _dbContext.SaveChangesAsync();

        return true;    
    }

    public async Task<SkillDto> GetSkill(int skillId)
    {
        var result = await _dbContext.Skills
            .FirstOrDefaultAsync(e => e.Id == skillId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Skill not found.");
        }    
        
        var response = new SkillDto{
            Id = result.Id,
            Skill1 = result.Skill1,
            UserId = result.UserId,
            IsActive = result.IsActive
        };

        return response;
    }

    public async Task<IEnumerable<SkillDto>> GetSkills(int userId)
    {
        var data = await (from skill in _dbContext.Skills
                where skill.IsActive == true && skill.UserId == userId
                select new SkillDto
                {
                    Id = skill.Id,
                    Skill1 = skill.Skill1,
                    UserId = skill.UserId,
                    IsActive = skill.IsActive
                }).ToListAsync();

        return data;    
    }

    public async Task<SkillDto> UpdateSkill(SkillDto skill)
    {
        var result = await _dbContext.Skills
            .FirstOrDefaultAsync(e => e.Id == skill.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Skill not found.");
        }        
        
        result.Skill1 = skill.Skill1;
        
        _dbContext.Skills.Update(result);

        await _dbContext.SaveChangesAsync();

        var response = new SkillDto{
            Id = result.Id,
            Skill1 = result.Skill1,
            IsActive = result.IsActive
        };

        return response;
    }
}
