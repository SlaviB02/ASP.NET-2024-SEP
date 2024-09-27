using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController(CinemaDbContext context) : Controller
    {
        public  IActionResult Index()
        {
            var cinemas=context
                .Cinemas
                .OrderBy (c => c.Location)
                .ThenBy (c => c.Name)
                .ToList();


            return View(cinemas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CinemaCreateViewModel  input)
        {
            if(!ModelState.IsValid)
            {
                return View(input);
            }
            Cinema cinema = new Cinema()
            {
                Name = input.Name,
                Location = input.Location,
            };

            context.Cinemas.Add(cinema);
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Details(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }

            var cinema=context
                .Cinemas
                .Include(cm=>cm.CinemaMovies)
                .ThenInclude(m=>m.Movie)
                .FirstOrDefault(c=>c.Id==guidId);

            if (cinema==null)
            {
                return RedirectToAction("Index");
            }

            var cinemaModel = new CinemaDetailsViewModel
            {
                Id = cinema.Id.ToString(),
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies.Select(cm => new MovieProgramView
                {
                    Title = cm.Movie.Title,
                    Duration = cm.Movie.Duration,
                })
                .ToList()
            };

            return View(cinemaModel);
        }
        
    }
}
