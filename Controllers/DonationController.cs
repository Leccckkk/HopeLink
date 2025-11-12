using Microsoft.AspNetCore.Mvc;
using WebsiteCharity.Data;
using WebsiteCharity.Models;

namespace WebsiteCharity.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Donate() => View();
        public IActionResult Receipt(int id)
        {
            var donation = _context.Donations.FirstOrDefault(d => d.Id == id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }
        [HttpPost]
        public IActionResult Donate(Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.Date = DateTime.Now;
                _context.Donations.Add(donation);
                _context.SaveChanges();

                return RedirectToAction("Receipt", new { id = donation.Id });
            }
            return View(donation);
        }
    }
}
