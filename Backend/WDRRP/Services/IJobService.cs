namespace WDRRP.Services;

public interface IJobService
{
    Task<IEnumerable<JobDto>> GetJobsUser(int userId);
    Task<IEnumerable<JobDto>> GetJobs();
    Task<JobDto> GetJob(int jobId);
    Task<JobDto> AddJob(JobDto job);
    Task<JobDto> UpdateJob(JobDto job);
    Task<bool> DeleteJob(JobDto job);
}
