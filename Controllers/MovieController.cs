using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Data;
using MovieApplication.Models;

namespace MovieApplication.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var movies = _db.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadGenres();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.DateAdded = DateTime.Now;
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            LoadGenres(movie.GenreId);
            return View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            LoadGenres(movie.GenreId);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Movies.Update(movie);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            LoadGenres(movie.GenreId);
            return View(movie);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _db.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void LoadGenres(Guid? selected = null)
        {
            ViewBag.Genres = new SelectList(_db.Genres.OrderBy(g => g.Name).ToList(), "Id", "Name", selected);
        }
    }
}
