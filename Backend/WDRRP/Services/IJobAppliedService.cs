using System;
using WDRRP.Dtos;

namespace WDRRP.Services;

public interface IJobAppliedService
{
    Task<IEnumerable<JobAppliedDto>> GetJobAppyUser(int userId);
    Task<IEnumerable<JobAppliedDto>> GetJobAppyJob(int jobId);
    Task<JobAppliedDto> GetJobAppy(int jobappyId);
    Task<JobAppliedDto> AddJobAppy(JobAppliedDto job);
    Task<JobAppliedDto> UpdateJobAppyStatus(StatusDto job);
    //Task<bool> DeleteJob(JobAppliedDto job);
}
