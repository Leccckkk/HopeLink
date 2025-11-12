using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebsiteCharity.Data;
using WebsiteCharity.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ? Add EF Core + MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

// ? Enable Sessions
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// ? Use session middleware
app.UseSession();

app.MapDefaultControllerRoute();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // ? Check if an admin exists, otherwise create one
    if (!db.Users.Any(u => u.IsAdmin))
    {
        var admin = new User
        {
            Name = "Admin",
            Email = "admin@charity.com",
            IsAdmin = true
        };

        var hasher = new PasswordHasher<User>();
        admin.Password = hasher.HashPassword(admin, "admin123");

        db.Users.Add(admin);
        db.SaveChanges();
        Console.WriteLine("? Default admin account created (admin@charity.com / admin123)");
    }
}
app.Run();
