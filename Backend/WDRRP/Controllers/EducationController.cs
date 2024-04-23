using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;

    public EducationController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult> GetEducations([FromRoute]int id)
    {
        try
        {
            return Ok(await _educationService.GetEducations(id));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get educations.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EducationDto>> GetEducation([FromRoute]int id)
    {
        try
        {
            var result = await _educationService.GetEducation(id);

            if (result == null) return NotFound();

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get education.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddEducation([FromBody]EducationDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest();

                await _educationService.AddEducation(dto);

            return Ok("Education added.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't add education.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateEducation([FromRoute]int id, [FromBody]EducationDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Education mismatch.");

            var educationToUpdate = await _educationService.GetEducation(id);

            if (educationToUpdate == null)
                return NotFound($"Education not found.");

            var Update = await _educationService.UpdateEducation(dto);
            return Ok("Education updated.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't update education.");
        }
    }
}
