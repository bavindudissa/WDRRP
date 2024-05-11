
using Microsoft.EntityFrameworkCore;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class JobRepository : IJobService
{
    private readonly WdrrpContext _dbContext;

    public JobRepository(WdrrpContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JobDto> AddJob(JobDto job)
    {
        var newJob = new Job{
            Title = job.Title,
            Company = job.Company,
            WorkplaceType = job.WorkplaceType,
            Location = job.Location,
            JobType = job.JobType,
            Description = job.Description,
            ApplicantCollectEmail = job.ApplicantCollectEmail,
            UserId = job.UserId,
            CreatedBy = 0,
            CreatedAt = DateTime.Now,
            UpdatedBy = null,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.Jobs.AddAsync(newJob);
        await _dbContext.SaveChangesAsync();
        
        var response = new JobDto{
            Id = newJob.Id,
            Title = newJob.Title,
            Company = newJob.Company,
            WorkplaceType = newJob.WorkplaceType,
            Location = newJob.Location,
            JobType = newJob.JobType,
            Description = newJob.Description,
            ApplicantCollectEmail = newJob.ApplicantCollectEmail,
            UserId = newJob.UserId,
            IsActive = true
        };

        return response;
    }

    public async Task<bool> DeleteJob(JobDto job)
    {
        var result = await _dbContext.Jobs
            .FirstOrDefaultAsync(e => e.Id == job.Id);

        if (result == null)
        {
            throw new InvalidOperationException("Job not found.");
        }

        result.UpdatedBy = 0; //This will replace the jwt token claim userId
        result.UpdatedAt = DateTime.Now;
        result.IsActive = false;

        _dbContext.Jobs.Update(result);

        await _dbContext.SaveChangesAsync();

        return true;    
    }

    public async Task<JobDto> GetJob(int jobId)
    {
        var result = await _dbContext.Jobs
            .FirstOrDefaultAsync(e => e.Id == jobId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Job not found.");
        }    
        
        var response = new JobDto{
            Id = result.Id,
            Title = result.Title,
            Company = result.Company,
            WorkplaceType = result.WorkplaceType,
            Location = result.Location,
            JobType = result.JobType,
            Description = result.Description,
            ApplicantCollectEmail = result.ApplicantCollectEmail,
            UserId = result.UserId,
            IsActive = true
        };

        return response;
    }

    public async Task<IEnumerable<JobDto>> GetJobs()
    {
        var data = await (from job in _dbContext.Jobs
            where job.IsActive == true //&& skill.UserId == userId
            select new JobDto{
                Id = job.Id,
                Title = job.Title,
                Company = job.Company,
                WorkplaceType = job.WorkplaceType,
                Location = job.Location,
                JobType = job.JobType,
                Description = job.Description,
                ApplicantCollectEmail = job.ApplicantCollectEmail,
                UserId = job.UserId,
                IsActive = true
            }).ToListAsync();

        return data;
    }

    public async Task<IEnumerable<JobDto>> GetJobsUser(int userId)
    {
        var data = await (from job in _dbContext.Jobs
            where job.IsActive == true && job.UserId == userId
            select new JobDto{
                Id = job.Id,
                Title = job.Title,
                Company = job.Company,
                WorkplaceType = job.WorkplaceType,
                Location = job.Location,
                JobType = job.JobType,
                Description = job.Description,
                ApplicantCollectEmail = job.ApplicantCollectEmail,
                UserId = job.UserId,
                IsActive = true
            }).ToListAsync();

        return data;
    }

    public async Task<JobDto> UpdateJob(JobDto job)
    {
        var result = await _dbContext.Jobs
            .FirstOrDefaultAsync(e => e.Id == job.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Job not found.");
        }        
        
        result.Title = job.Title;
        result.Company = job.Company;
        result.WorkplaceType = job.WorkplaceType;
        result.Location = job.JobType;
        result.Description = job.Description;
        result.ApplicantCollectEmail = job.ApplicantCollectEmail;
        result.Location = job.Location;
        
        _dbContext.Jobs.Update(result);

        await _dbContext.SaveChangesAsync();

          var response = new JobDto{
            Id = result.Id,
            Title = result.Title,
            Company = result.Company,
            WorkplaceType = result.WorkplaceType,
            Location = result.Location,
            JobType = result.JobType,
            Description = result.Description,
            ApplicantCollectEmail = result.ApplicantCollectEmail,
            UserId = result.UserId,
            IsActive = true
        };

        return response;
    }
}
