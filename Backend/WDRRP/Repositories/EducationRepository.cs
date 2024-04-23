using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class EducationRepository : IEducationService
{
    private readonly WdrrpContext _dbContext;
    public EducationRepository(WdrrpContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<EducationDto> AddEducation(EducationDto education)
    {
        var newEducation = new Education{
            School = education.School,
            Degree = education.Degree,
            FieldOfStudy = education.FieldOfStudy,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            Grade = education.Grade,
            UserId = education.UserId,
            CreatedBy = 0,
            CreatedAt = DateTime.Now,
            UpdatedBy = null,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.Educations.AddAsync(newEducation);
        await _dbContext.SaveChangesAsync();

        var response = new EducationDto{
            Id = newEducation.Id,
            School = newEducation.School,
            Degree = newEducation.Degree,
            FieldOfStudy = newEducation.FieldOfStudy,
            StartDate = newEducation.StartDate,
            EndDate = newEducation.EndDate,
            Grade = newEducation.Grade,
            UserId = newEducation.UserId,
            IsActive = newEducation.IsActive
        };

        return response;
    }

    public async Task<bool> DeleteEducation(EducationDto education)
    {
        var result = await _dbContext.Educations
            .FirstOrDefaultAsync(e => e.Id == education.Id);

        if (result == null)
        {
            throw new InvalidOperationException("Education not found.");
        }

        result.UpdatedBy = 0; //This will replace the jwt token claim userId
        result.UpdatedAt = DateTime.Now;
        result.IsActive = false;

        _dbContext.Educations.Update(result);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<EducationDto> GetEducation(int educationId)
    {
        var result = await _dbContext.Educations
            .FirstOrDefaultAsync(e => e.Id == educationId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Education not found.");
        }    
        
        var response = new EducationDto{
            Id = result.Id,
            School = result.School,
            Degree = result.Degree,
            FieldOfStudy = result.FieldOfStudy,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Grade = result.Grade,
            UserId = result.UserId,
            IsActive = result.IsActive
        };

        return response;    
    }

    public async Task<IEnumerable<EducationDto>> GetEducations(int userId)
    {
        var data = await (from education in _dbContext.Educations
                where education.IsActive == true && education.UserId == userId
                select new EducationDto
                {
                    Id = education.Id,
                    School = education.School,
                    Degree = education.Degree,
                    FieldOfStudy = education.FieldOfStudy,
                    StartDate = education.StartDate,
                    EndDate = education.EndDate,
                    Grade = education.Grade,
                    UserId = education.UserId,
                    IsActive = education.IsActive
                }).ToListAsync();

        return data;        
    }

    public async Task<EducationDto> UpdateEducation(EducationDto education)
    {
        var result = await _dbContext.Educations
            .FirstOrDefaultAsync(e => e.Id == education.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Education not found.");
        }        

        result.School = education.School;
        result.Degree = education.Degree;
        result.FieldOfStudy = education.FieldOfStudy;
        result.StartDate = education.StartDate;
        result.EndDate = education.EndDate;
        result.Grade = education.Grade;

        _dbContext.Educations.Update(result);

        await _dbContext.SaveChangesAsync();

        var response = new EducationDto{
            Id = result.Id,
            School = result.School,
            Degree = result.Degree,
            FieldOfStudy = result.FieldOfStudy,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Grade = result.Grade,
            UserId = result.UserId,
            IsActive = result.IsActive
        };

        return response;
    }
}
