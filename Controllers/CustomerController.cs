using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Data;
using MovieApplication.Models;

namespace MovieApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var customers = _db.Customers
                .Include(c => c.Membershiptype)
                .Include(c => c.Movies)
                .ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadMembershipTypes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer c)
        {
            if (_db.Customers.Any(x => x.Name == c.Name))
            {
                ModelState.AddModelError(nameof(Customer.Name), "A customer with this name already exists.");
            }
            if (!ModelState.IsValid)
            {
                LoadMembershipTypes();
                return View(c);
            }
            _db.Customers.Add(c);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void LoadMembershipTypes()
        {
            var members = _db.MemberShipTypes.OrderBy(m => m.DurationInMonth).ToList();
            ViewBag.member = members.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });
        }
    }
}
