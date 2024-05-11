using Microsoft.AspNetCore.Mvc;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetJobs()
    {
        try
        {
            return Ok(await _jobService.GetJobs());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get jobs.");
        }
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult> GetJobsUser([FromRoute]int id)
    {
        try
        {
            return Ok(await _jobService.GetJobsUser(id));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get jobs.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobDto>> GetJob([FromRoute]int id)
    {
        try
        {
            var result = await _jobService.GetJob(id);

            if (result == null) return NotFound();

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get job.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddJob([FromBody]JobDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest();

                await _jobService.AddJob(dto);

            return Ok("Job added.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't add job.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateJob([FromRoute]int id, [FromBody]JobDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Job mismatch.");

            var skillToUpdate = await _jobService.GetJob(id);

            if (skillToUpdate == null)
                return NotFound($"Job not found.");

            var Update = await _jobService.UpdateJob(dto);
            return Ok("Job updated.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't update job.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteJob([FromRoute]int id)
    {
        try
        {
            var jobToDelete = await _jobService.GetJob(id);


            var result = await _jobService.DeleteJob(jobToDelete);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't delete job.");
        }
    }
}
