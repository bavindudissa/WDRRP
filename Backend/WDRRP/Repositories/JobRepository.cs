
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class JobRepository : IJobService
{
    private readonly WdrrpContext _dbContext;
     private readonly HttpClient _httpClient;

    public JobRepository(WdrrpContext dbContext, HttpClient httpClient)
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
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
            UserName = (from user in _dbContext.Users where user.Id == result.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
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
                UserName = (from user in _dbContext.Users where user.Id == job.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
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
                UserName = (from user in _dbContext.Users where user.Id == job.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
                IsActive = true
            }).ToListAsync();

        return data;
    }

    public async Task<IEnumerable<JobDto>> GetLatestJobs()
    {
        var data = await (from job in _dbContext.Jobs
                        where job.IsActive == true
                        orderby job.CreatedAt descending // Assuming you have a CreatedAt field
                        select new JobDto
                        {
                            Id = job.Id,
                            Title = job.Title,
                            Company = job.Company,
                            WorkplaceType = job.WorkplaceType,
                            Location = job.Location,
                            JobType = job.JobType,
                            Description = job.Description,
                            ApplicantCollectEmail = job.ApplicantCollectEmail,
                            UserId = job.UserId,
                            UserName = (from user in _dbContext.Users where user.Id == job.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
                            IsActive = true
                        }).Take(6).ToListAsync();

        return data;
    }

    public async Task<int> JobCount()
    {
        var data = await (from job in _dbContext.Jobs
            where job.IsActive == true //&& skill.UserId == userId
            select job).CountAsync();

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

public async Task<Dictionary<string, List<JobDto>>> GetRecommendedJobs(int userId)
{
    // Step 1: Fetch user's skills
    var userSkills = await _dbContext.Skills
        .Where(s => s.UserId == userId && s.IsActive)
        .Select(s => s.Skill1)
        .ToListAsync();

    if (!userSkills.Any())
    {
        throw new InvalidOperationException("User has no active skills.");
    }

    // Step 2: Call the ML model API to get recommended jobs
    var skillsArray = userSkills.ToArray();
    var requestBody = JsonConvert.SerializeObject(new { skills = string.Join(", ", skillsArray) });
    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

    var response = await _httpClient.PostAsync("http://127.0.0.1:5000/recommend_jobs", content);
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception("Failed to get job recommendations from ML model.");
    }

    var result = await response.Content.ReadAsStringAsync();
    var recommendedJobs = JsonConvert.DeserializeObject<List<dynamic>>(result);

    // Step 3: Initialize dictionaries to store unique jobs for each category
    var categorizedJobs = new Dictionary<string, List<JobDto>>
    {
        { "Current Position", new List<JobDto>() },
        { "Alternative 1", new List<JobDto>() },
        { "Alternative 2", new List<JobDto>() }
    };

    foreach (var recJob in recommendedJobs)
    {
        // Titles for each category
        var currentPositionTitle = (string)recJob["Current Position"];
        var alternative1Title = (string)recJob["Alternative 1"];
        var alternative2Title = (string)recJob["Alternative 2"];

        // Fetch and add unique jobs for "Current Position"
        var currentJobs = await _dbContext.Jobs
            .Where(j => j.Title.Contains(currentPositionTitle) && j.IsActive && j.UserId != userId)
            .Select(j => new JobDto
            {
                Id = j.Id,
                Title = j.Title,
                Company = j.Company,
                WorkplaceType = j.WorkplaceType,
                Location = j.Location,
                JobType = j.JobType,
                Description = j.Description,
                ApplicantCollectEmail = j.ApplicantCollectEmail,
                UserId = j.UserId,
                UserName = (from user in _dbContext.Users where user.Id == j.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
                IsActive = true
            })
            .ToListAsync();

        categorizedJobs["Current Position"].AddRange(
            currentJobs.GroupBy(j => j.Id).Select(g => g.First())
        );

        // Fetch and add unique jobs for "Alternative 1"
        var alternative1Jobs = await _dbContext.Jobs
            .Where(j => j.Title.Contains(alternative1Title) && j.IsActive && j.UserId != userId)
            .Select(j => new JobDto
            {
                Id = j.Id,
                Title = j.Title,
                Company = j.Company,
                WorkplaceType = j.WorkplaceType,
                Location = j.Location,
                JobType = j.JobType,
                Description = j.Description,
                ApplicantCollectEmail = j.ApplicantCollectEmail,
                UserId = j.UserId,
                UserName = (from user in _dbContext.Users where user.Id == j.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
                IsActive = true
            })
            .ToListAsync();

        categorizedJobs["Alternative 1"].AddRange(
            alternative1Jobs.GroupBy(j => j.Id).Select(g => g.First())
        );

        // Fetch and add unique jobs for "Alternative 2"
        var alternative2Jobs = await _dbContext.Jobs
            .Where(j => j.Title.Contains(alternative2Title) && j.IsActive && j.UserId != userId)
            .Select(j => new JobDto
            {
                Id = j.Id,
                Title = j.Title,
                Company = j.Company,
                WorkplaceType = j.WorkplaceType,
                Location = j.Location,
                JobType = j.JobType,
                Description = j.Description,
                ApplicantCollectEmail = j.ApplicantCollectEmail,
                UserId = j.UserId,
                UserName = (from user in _dbContext.Users where user.Id == j.UserId select user.FirstName + " " + user.LastName).FirstOrDefault(),
                IsActive = true
            })
            .ToListAsync();

        categorizedJobs["Alternative 2"].AddRange(
            alternative2Jobs.GroupBy(j => j.Id).Select(g => g.First())
        );
    }

    // Step 4: Remove any duplicates across the entire dictionary
    foreach (var key in categorizedJobs.Keys)
    {
        categorizedJobs[key] = categorizedJobs[key].GroupBy(j => j.Id).Select(g => g.First()).ToList();
    }

    return categorizedJobs;
}

}
