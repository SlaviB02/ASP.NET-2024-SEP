using CinemaApp.Data;
using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private CinemaDbContext context;
        public MovieController(CinemaDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
           IEnumerable<Movie> movies=context.Movies.ToList();
            return View(movies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            bool isValidGuid=Guid.TryParse(id, out Guid guidId);
            if(!isValidGuid)
            {
                return RedirectToAction("Index");
            }

            Movie movie = context.Movies.FirstOrDefault(m => m.Id == guidId)!;
            if(movie == null)
            {
                return RedirectToAction("Index");
            }
            return View(movie);
        }
    }
}
