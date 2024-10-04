using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models;
using System.Threading.Tasks;
namespace UserManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<ApplicationUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync(); // Retrieve users from the database
        }

        public async Task<IActionResult> OnPostBlockAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100); // Simulate block
                await _userManager.UpdateAsync(user);
            }
            return RedirectToPage("/UserManagement");
        }
    }
}
