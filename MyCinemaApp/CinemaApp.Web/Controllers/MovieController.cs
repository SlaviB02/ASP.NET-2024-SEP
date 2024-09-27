using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
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
        [HttpGet]
        public IActionResult AddToProgram(string movieId)
        {
            bool isValidGuid = Guid.TryParse(movieId, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }
            var movie = context.Movies.Find(guidId);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            var cinemas = context.Cinemas.ToList();

            var viewModel = new AddMovieToCinemaProgram
            {
                MovieId = movieId,
                MovieTitle = movie.Title,
                Cinemas = cinemas.Select(c => new CinemaCheckBoxItem
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                    IsSelected = false
                })
                .ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddToProgram(AddMovieToCinemaProgram model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existingCinemaMovies=context.CinemaMovies
                .Where(cm=>cm.MovieId.ToString()==model.MovieId)
                .ToList();

            context.RemoveRange(existingCinemaMovies);


            foreach(var cinema in model.Cinemas)
            {
                if(cinema.IsSelected)
                {
                    var cinemaMovie = new CinemaMovie
                    {
                        CinemaId = Guid.Parse(cinema.Id),
                        MovieId = Guid.Parse(model.MovieId),
                    };
                    context.CinemaMovies.Add(cinemaMovie);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}
