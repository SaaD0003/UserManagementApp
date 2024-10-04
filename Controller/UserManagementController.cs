using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UserManagementApp.Models;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class UserManagementController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManagementController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var userData = users.Select(u => new
        {
            u.Id,
            u.UserName,
            u.Email,
            LastLoginTime = u.LastLoginTime, // Ensure this property exists in your ApplicationUser class
            Status = u.LockoutEnd.HasValue ? "Blocked" : "Active"
        });
        return Json(userData);
    }
}
