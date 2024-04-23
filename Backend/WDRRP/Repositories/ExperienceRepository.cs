using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class ExperienceRepository : IExperienceService
{
    private readonly WdrrpContext _dbContext;
    public ExperienceRepository(WdrrpContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExperienceDto> AddExperience(ExperienceDto experience)
    {
        var newExperience = new Experience{
            Title = experience.Title,
            EmploymentTypeId = experience.EmploymentTypeId,
            CompanyName = experience.CompanyName,
            Location = experience.Location,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description,
            Industry = experience.Industry,
            UserId = experience.UserId,
            CreatedBy = 0,
            CreatedAt = DateTime.Now,
            UpdatedBy = null,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.Experiences.AddAsync(newExperience);
        await _dbContext.SaveChangesAsync();

        var response = new ExperienceDto{
            Id = newExperience.Id,
            Title = newExperience.Title,
            EmploymentTypeId = newExperience.EmploymentTypeId,
            CompanyName = newExperience.CompanyName,
            Location = newExperience.Location,
            StartDate = newExperience.StartDate,
            EndDate = newExperience.EndDate,
            Description = newExperience.Description,
            Industry = newExperience.Industry,
            UserId = newExperience.UserId,
            IsActive = newExperience.IsActive,
        };

        return response;
    }

    public async Task<bool> DeleteExperience(ExperienceDto experience)
    {
         var result = await _dbContext.Experiences
            .FirstOrDefaultAsync(e => e.Id == experience.Id);

        if (result == null)
        {
            throw new InvalidOperationException("Experience not found.");
        }

        result.UpdatedBy = 0; //This will replace the jwt token claim userId
        result.UpdatedAt = DateTime.Now;
        result.IsActive = false;

        _dbContext.Experiences.Update(result);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<ExperienceDto> GetExperience(int experienceId)
    {
        var result = await _dbContext.Experiences
            .FirstOrDefaultAsync(e => e.Id == experienceId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Experience not found.");
        }

        var response = new ExperienceDto{
            Id = result.Id,
            Title = result.Title,
            EmploymentTypeId = result.EmploymentTypeId,
            CompanyName = result.CompanyName,
            Location = result.Location,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Description = result.Description,
            Industry = result.Industry,
            UserId = result.UserId,
            IsActive = result.IsActive,
        };

        return response;
    }

    public async Task<IEnumerable<ExperienceDto>> GetExperiences(int userId)
    {
         var data = await (from experience in _dbContext.Experiences
                where experience.IsActive == true && experience.UserId == userId
                select new ExperienceDto
                {
                    Id = experience.Id,
                    Title = experience.Title,
                    EmploymentTypeId = experience.EmploymentTypeId,
                    CompanyName = experience.CompanyName,
                    Location = experience.Location,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    Description = experience.Description,
                    Industry = experience.Industry,
                    UserId = experience.UserId,
                    IsActive = experience.IsActive,
                }).ToListAsync();

        return data; 
    }

    public async Task<ExperienceDto> UpdateExperience(ExperienceDto experience)
    {
        var result = await _dbContext.Experiences
            .FirstOrDefaultAsync(e => e.Id == experience.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Experience not found.");
        }

        result.Title = experience.Title;
        result.EmploymentTypeId = experience.EmploymentTypeId;
        result.CompanyName = experience.CompanyName;
        result.Location = experience.Location;
        result.StartDate = experience.StartDate;
        result.EndDate = experience.EndDate;
        result.Description = experience.Description;
        result.Industry = experience.Industry;

        _dbContext.Experiences.Update(result);

        await _dbContext.SaveChangesAsync();

        var response = new ExperienceDto{
            Id = result.Id,
            Title = result.Title,
            EmploymentTypeId = result.EmploymentTypeId,
            CompanyName = result.CompanyName,
            Location = result.Location,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Description = result.Description,
            Industry = result.Industry,
            UserId = result.UserId,
            IsActive = result.IsActive,
        };

        return response;
    }
}
