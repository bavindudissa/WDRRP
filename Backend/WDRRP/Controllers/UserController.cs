using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        try
        {
            return Ok(await _userService.GetUsers());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get users.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute]int id)
    {
        try
        {
            var result = await _userService.GetUser(id);

            if (result == null) return NotFound();

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get user.");
        }
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<UserDto>> SearchUsers([FromRoute]string name)
    {
        try
        {
            var result = await _userService.SearchUsers(name);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get user.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody]UserDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest();

                await _userService.AddUser(dto);

            return Ok("User added.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't add user.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser([FromRoute]int id, [FromBody]UserDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("User mismatch.");

            var userToUpdate = await _userService.GetUser(id);

            if (userToUpdate == null)
                return NotFound($"User not found.");

            var Update = await _userService.UpdateUser(dto);
            return Ok("User updated.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't update user.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody]LoginDto dto)
    {
        try
        {
            if (dto.Email == null || dto.Password == null)
                return BadRequest();

            var result =   await _userService.Login(dto.Email, dto.Password);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Invalid credintials");
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult> UserCount()
    {
        try
        {
            return Ok(await _userService.UserCount());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can't get users.");
        }
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeDto dto)
    {
        try
        {

            var result = await _userService.ChangePassword(dto);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassowrd([FromBody] ForgotPasswordDto dto)
    {
        try
        {
            var result = await _userService.ForgotPassword(dto);
            if (result == false)return NotFound();
            return Ok("Rest password link sent.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] PasswordRestDto dto)
    {
        try
        {
            var result = await _userService.ResetPassword(dto);
            if (result == false)return NotFound();
            return Ok("Rest password sucessful.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
