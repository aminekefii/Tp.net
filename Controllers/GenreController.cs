using Microsoft.AspNetCore.Mvc;
using MovieApplication.Data;
using MovieApplication.Models;

namespace MovieApplication.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }




        public IActionResult Index()
        {
            var genres = _db.Genres.ToList();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Genres.Add(genre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }
    }
}
