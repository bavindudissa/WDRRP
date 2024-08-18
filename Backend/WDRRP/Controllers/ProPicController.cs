using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProPicController : ControllerBase
    {
        private readonly IProfilePicService _profilePicService;

        public ProPicController(IProfilePicService profilePicService)
        {
            _profilePicService = profilePicService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfilePicDto>> GetProfilePicture([FromRoute]int id)
        {
            try
            {
                var result = await _profilePicService.GetProfilePicture(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Can't get profile picture.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProfilePicture([FromForm]ProfilePicDto dto)
        {
           // try
            {
                if (dto == null)
                    return BadRequest();

                    await _profilePicService.AddProfilePicture(dto);

                return Ok("Profile picture added.");
            }
           // catch (Exception)
           // {
            //     return StatusCode(StatusCodes.Status500InternalServerError, "Can't add profile picture.");
           // }
        }
    }
}
