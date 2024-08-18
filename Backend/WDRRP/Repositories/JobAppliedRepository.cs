using System;
using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class JobAppliedRepository : IJobAppliedService
{
    private readonly WdrrpContext _dbContext;
    private readonly IFileService _fileService;
    private readonly IEmailService _emailService;

    public JobAppliedRepository(WdrrpContext dbContext, IFileService fileService, IEmailService emailService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
        _emailService = emailService;
    }

    public async Task<JobAppliedDto> AddJobAppy(JobAppliedDto job)
    {
        // Check if the user has already applied for the job
        var existingApplication = await _dbContext.JobApplieds
            .FirstOrDefaultAsync(ja => ja.JobId == job.jobId && ja.UserId == job.userId);

        if (existingApplication != null)
        {
            throw new InvalidOperationException("You have already applied for this job.");
        }
        
        string? dbPath = "";
        //var hasImage = agent.image;
        if (job.file != null || job.file.Length != 0)
        {
            dbPath = await _fileService.SaveFileAsync(job.file, job.file.ContentType);
        }

        var newJobApply = new JobApplied{
            JobId = job.jobId,
            UserId = job.userId,
            Url = dbPath,
            Status = "Applied",
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.JobApplieds.AddAsync(newJobApply);
        await _dbContext.SaveChangesAsync();

        var response = new JobAppliedDto{
            id = newJobApply.Id,
            jobId = job.jobId,
            userId = job.userId,
            url = dbPath,
            status = newJobApply.Status,
            isActive = true
        };

        return response;
    }

    public async Task<JobAppliedDto> GetJobAppy(int jobappyId)
    {
        var result = await _dbContext.JobApplieds
            .FirstOrDefaultAsync(e => e.Id == jobappyId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Job not found.");
        }

        var job = await _dbContext.Jobs
            .FirstOrDefaultAsync(e => e.Id == result.JobId);

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == result.UserId);

        // Extracting skills from job description, accounting for line breaks
        var jobSkills = job.Description?
            .Split(new char[] { ' ', ',', '.', ';', ':', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().ToLower())
            .Distinct()
            .ToList();

        // Getting user skills
        var userSkills = await _dbContext.Skills
            .Where(s => s.UserId == user.Id && s.IsActive)
            .Select(s => s.Skill1.Trim().ToLower())
            .ToListAsync();

        // Calculating the match percentage
        int matchedSkills = userSkills.Intersect(jobSkills).Count();
        float matchPercentage = userSkills.Count > 0 ? (float)matchedSkills / userSkills.Count * 100 : 0;

        var jobDto = new JobDto
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
            UserName = (from userPost in _dbContext.Users where userPost.Id == job.UserId select userPost.FirstName + " " + userPost.LastName).FirstOrDefault(),
            IsActive = true
        };

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ContactNumber = user.ContactNumber,
            Password = user.Password,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            UserTypeId = user.UserTypeId,
            UserStatusId = user.UserStatusId,
            IsActive = user.IsActive,
            ProfilePic = user.ProfilePic,
        };

        var response = new JobAppliedDto
        {
            id = result.Id,
            jobId = result.JobId,
            userId = result.UserId,
            url = result.Url,
            status = result.Status,
            isActive = result.IsActive,
            jobDto = jobDto,
            userDto = userDto,
            matchPercentage = matchPercentage
        };

        return response;
    }

    public async Task<IEnumerable<JobAppliedDto>> GetJobAppyJob(int jobId)
    {
        var results = await _dbContext.JobApplieds
            .Where(e => e.JobId == jobId && e.IsActive == true)
            .ToListAsync();

        if (!results.Any())
        {
            throw new InvalidOperationException("No job applications found for this job.");
        }

        var job = await _dbContext.Jobs
            .FirstOrDefaultAsync(e => e.Id == jobId);

        var jobDto = new JobDto
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
            UserName = (from userPost in _dbContext.Users where userPost.Id == job.UserId select userPost.FirstName + " " + userPost.LastName).FirstOrDefault(),
            IsActive = true
        };

        var response = new List<JobAppliedDto>();

        var jobSkills = job.Description?
            .Split(new char[] { ' ', ',', '.', ';', ':', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().ToLower())
            .Distinct()
            .ToList();

        foreach (var result in results)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Id == result.UserId);

            var userSkills = await _dbContext.Skills
                .Where(s => s.UserId == user.Id && s.IsActive)
                .Select(s => s.Skill1.Trim().ToLower())
                .ToListAsync();

            int matchedSkills = userSkills.Intersect(jobSkills).Count();
            float matchPercentage = userSkills.Count > 0 ? (float)matchedSkills / userSkills.Count * 100 : 0;

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNumber = user.ContactNumber,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                UserTypeId = user.UserTypeId,
                UserStatusId = user.UserStatusId,
                IsActive = user.IsActive,
                ProfilePic = user.ProfilePic,
            };

            response.Add(new JobAppliedDto
            {
                id = result.Id,
                jobId = result.JobId,
                userId = result.UserId,
                url = result.Url,
                status = result.Status,
                isActive = result.IsActive,
                jobDto = jobDto,
                userDto = userDto,
                matchPercentage = matchPercentage
            });
        }

        return response;
    }

    public async Task<IEnumerable<JobAppliedDto>> GetJobAppyUser(int userId)
    {
        var results = await _dbContext.JobApplieds
            .Where(e => e.UserId == userId && e.IsActive == true)
            .ToListAsync();

        if (!results.Any())
        {
            throw new InvalidOperationException("No job applications found for this user.");
        }

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == userId);

        var userSkills = await _dbContext.Skills
            .Where(s => s.UserId == user.Id && s.IsActive)
            .Select(s => s.Skill1.Trim().ToLower())
            .ToListAsync();

        var response = new List<JobAppliedDto>();

        foreach (var result in results)
        {
            var job = await _dbContext.Jobs
                .FirstOrDefaultAsync(e => e.Id == result.JobId);

            var jobSkills = job.Description?
                .Split(new char[] { ' ', ',', '.', ';', ':', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower())
                .Distinct()
                .ToList();

            int matchedSkills = userSkills.Intersect(jobSkills).Count();
            float matchPercentage = userSkills.Count > 0 ? (float)matchedSkills / userSkills.Count * 100 : 0;

            var jobDto = new JobDto
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
                UserName = (from userPost in _dbContext.Users where userPost.Id == job.UserId select userPost.FirstName + " " + userPost.LastName).FirstOrDefault(),
                IsActive = true
            };

            response.Add(new JobAppliedDto
            {
                id = result.Id,
                jobId = result.JobId,
                userId = result.UserId,
                url = result.Url,
                status = result.Status,
                isActive = result.IsActive,
                jobDto = jobDto,
                userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber,
                    Password = user.Password,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    UserTypeId = user.UserTypeId,
                    UserStatusId = user.UserStatusId,
                    IsActive = user.IsActive,
                    ProfilePic = user.ProfilePic,
                },
                matchPercentage = matchPercentage
            });
        }

        return response;
    }


    public async Task<JobAppliedDto> UpdateJobAppyStatus(StatusDto job)
    {
        var result = await _dbContext.JobApplieds
            .FirstOrDefaultAsync(e => e.Id == job.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Job not found.");
        }   

        result.Status = job.Status;

        _dbContext.JobApplieds.Update(result);

        await _dbContext.SaveChangesAsync();

        var jobEntity = await _dbContext.Jobs
        .FirstOrDefaultAsync(e => e.Id == result.JobId);

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == result.UserId);

        var subject = "Job Apply Update";
        
            var body = $@"
        <html>
        <body>
            <p>Dear Applicant,</p>
            <p>Your application status for the job '<strong>{jobEntity.Title}</strong>' at '<strong>{jobEntity.Company}</strong>' has been updated.</p>
            <h3>Job Details:</h3>
            <ul>
                <li><strong>Title:</strong> {jobEntity.Title}</li>
                <li><strong>Company:</strong> {jobEntity.Company}</li>
                <li><strong>Workplace Type:</strong> {jobEntity.WorkplaceType}</li>
                <li><strong>Location:</strong> {jobEntity.Location}</li>
                <li><strong>Job Type:</strong> {jobEntity.JobType}</li>
                <li><strong>Description:</strong> {jobEntity.Description}</li>
                <li><strong>Applicant Collection Email:</strong> {jobEntity.ApplicantCollectEmail}</li>
            </ul>
            <p><strong>Current Status:</strong> {result.Status}</p>
            <p>Thank you for applying.</p>
            <p>Best regards,<br>Jobzen</p>
        </body>
        </html>
    ";

        _emailService.SendEmailAsync(user.Email, subject, body);

        var response = new JobAppliedDto{
            id = result.Id,
            jobId = result.JobId,
            userId = result.UserId,
            url = result.Url,
            status = result.Status,
            isActive = true
        };

        return response;
    }
}
