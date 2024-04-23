using Microsoft.AspNetCore.Mvc;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class EmploymentTypeController : ControllerBase
{
    private readonly IEmploymentTypeService _emplymentTypeService;

    public EmploymentTypeController(IEmploymentTypeService emplymentTypeService)
    {
        _emplymentTypeService = emplymentTypeService;
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult> GetEmploymentTypes()
    {
        try
        {
            return Ok(await _emplymentTypeService.GetEmploymentTypes());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get skills.");
        }
    }
}
