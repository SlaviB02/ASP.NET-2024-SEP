using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
        public IActionResult Create(AddMovieInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            Movie movie = new Movie
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = DateTime.Parse(inputModel.ReleaseDate),
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description,
            };
         
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

        public IActionResult Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == guidId)!;
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
