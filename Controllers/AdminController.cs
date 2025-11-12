using Microsoft.AspNetCore.Mvc;
using WebsiteCharity.Data;

namespace WebsiteCharity.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // Check if logged in and admin
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin == null || isAdmin != "True")
            {
                return RedirectToAction("Login", "User");
            }

            _context.ChangeTracker.Clear();
            var totalDonations = _context.Donations.Sum(d => d.Amount);
            var totalUsers = _context.Users.Count();
            var allDonations = _context.Donations.ToList();
            var allUsers = _context.Users.ToList();

            ViewBag.TotalDonations = totalDonations;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.Donations = allDonations;
            ViewBag.Users = allUsers;

            return View();

        }

    }
}
