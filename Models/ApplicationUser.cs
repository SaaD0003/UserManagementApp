namespace UserManagementApp.Models
{
using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime LastLoginTime { get; set; }
} 
}
