using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult> GetSkills([FromRoute]int id)
    {
        try
        {
            return Ok(await _skillService.GetSkills(id));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get skills.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SkillDto>> GetSkill([FromRoute]int id)
    {
        try
        {
            var result = await _skillService.GetSkill(id);

            if (result == null) return NotFound();

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get skill.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddSkill([FromBody]SkillDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest();

                await _skillService.AddSkill(dto);

            return Ok("Skill added.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't add skill.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateSkill([FromRoute]int id, [FromBody]SkillDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Skill mismatch.");

            var skillToUpdate = await _skillService.GetSkill(id);

            if (skillToUpdate == null)
                return NotFound($"Skill not found.");

            var Update = await _skillService.UpdateSkill(dto);
            return Ok("Skill updated.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't update skill.");
        }
    }
}
