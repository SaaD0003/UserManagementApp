using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models; // Your ApplicationUser model namespace

var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL with EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add controllers with views (or Minimal APIs if preferred)
builder.Services.AddControllersWithViews();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Make sure this is before UseAuthorization
app.UseAuthorization();
// Set up endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// app.MapRazorPages();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
//     endpoints.MapRazorPages();

//     endpoints.MapGet("/", async context =>
//     {
//         if (context.User.Identity.IsAuthenticated)
//       {
//             context.Response.Redirect("/UserManagement");
//         }
//         else
//         {
//             context.Response.Redirect("/Login");
//         }
//     });
// }
// );



app.Run();
