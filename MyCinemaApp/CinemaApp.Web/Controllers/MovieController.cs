using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CinemaApp.Web.Controllers
{
    public class MovieController(CinemaDbContext context) : Controller
    {
        
        public async Task<IActionResult> Index()
        {
           IEnumerable<Movie> movies=await context.Movies.ToListAsync();
            return View(movies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
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
         
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            bool isValidGuid=Guid.TryParse(id, out Guid guidId);
            if(!isValidGuid)
            {
                return RedirectToAction("Index");
            }

            Movie? movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == guidId);
            if(movie == null)
            {
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }
            Movie? movie =await context.Movies.FirstOrDefaultAsync(m => m.Id == guidId);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

           context.Movies.Remove(movie);
           await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddToProgram(string movieId)
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

            var cinemas =await context.Cinemas.ToListAsync();

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
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaProgram model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existingCinemaMovies= await context.CinemaMovies
                .Where(cm=>cm.MovieId.ToString()==model.MovieId)
                .ToListAsync();

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
                   await context.CinemaMovies.AddAsync(cinemaMovie);
                }
            }
           await context.SaveChangesAsync();
            return RedirectToAction("Index");   
        }
    }
}
