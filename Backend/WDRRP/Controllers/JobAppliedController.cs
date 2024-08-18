using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAppliedController : ControllerBase
    {
        private readonly IJobAppliedService _jobService;

        public JobAppliedController(IJobAppliedService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult> GetJobAppyUser([FromRoute]int id)
        {
            try
            {
                return Ok(await _jobService.GetJobAppyUser(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Can't get job applies.");
            }
        }

        [HttpGet("job/{id:int}")]
        public async Task<ActionResult> GetJobAppyJob([FromRoute]int id)
        {
            try
            {
                return Ok(await _jobService.GetJobAppyJob(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Can't get job applies.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<JobAppliedDto>> GetJobAppy([FromRoute]int id)
        {
            try
            {
                var result = await _jobService.GetJobAppy(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Can't get job apply.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddJobAppy([FromForm]JobAppliedDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();

                    await _jobService.AddJobAppy(dto);

                return Ok("Job apply added.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateJobAppyStatus([FromRoute]int id, [FromBody]StatusDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("Job apply mismatch.");

                var skillToUpdate = await _jobService.GetJobAppy(id);

                if (skillToUpdate == null)
                    return NotFound($"Job apply not found.");

                var Update = await _jobService.UpdateJobAppyStatus(dto);
                return Ok("Job apply updated.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Can't update job apply.");
            }
        }
    }
}
