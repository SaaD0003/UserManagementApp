using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UserManagementApp.Models;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult>  GetUsers()
    {
        var users = _userManager.Users.Select(u => new {
            u.Id,
            u.Email,
            u.UserName,
            u.LastLoginTime,
            u.IsBlocked
        }).ToList();

        return Ok(users);
    }

    [HttpPost("block")]
    public async Task<IActionResult> BlockUsers([FromBody] List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }
        }

        return Ok();
    }

    [HttpPost("unblock")]
    public async Task<IActionResult> UnblockUsers([FromBody] List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }
        }

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUsers([FromBody] List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        return Ok();
    }
}
