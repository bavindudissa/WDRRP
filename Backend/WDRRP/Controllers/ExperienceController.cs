using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult> GetExperiences([FromRoute]int id)
    {
        try
        {
            return Ok(await _experienceService.GetExperiences(id));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get experience.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExperienceDto>> GetExperience([FromRoute]int id)
    {
        try
        {
            var result = await _experienceService.GetExperience(id);

            if (result == null) return NotFound();

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get experience.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddExperience([FromBody]ExperienceDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest();

                await _experienceService.AddExperience(dto);

            return Ok("Experience added.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't add experience.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateExperience([FromRoute]int id, [FromBody]ExperienceDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Experience mismatch.");

            var experienceToUpdate = await _experienceService.GetExperience(id);

            if (experienceToUpdate == null)
                return NotFound($"Experience not found.");

            var Update = await _experienceService.UpdateExperience(dto);
            return Ok("Experience updated.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't update experience.");
        }
    }
}
