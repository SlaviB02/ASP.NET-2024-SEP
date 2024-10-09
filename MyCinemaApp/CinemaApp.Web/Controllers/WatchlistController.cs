using CinemaApp.Data;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data.Models;
using System;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly CinemaDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public WatchlistController(CinemaDbContext _context, UserManager<IdentityUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            string userId = userManager.GetUserId(User)!;

            var movies =await context.UsersMovies
                .Where(x => x.UserId == userId)
                .Include(m=>m.Movie)
                .Select(um => new WatchlistViewModel
                {
                    Genre = um.Movie.Genre,
                    ImageUrl = um.Movie.ImageUrl!,
                    Title = um.Movie.Title,
                    MovieId = um.Movie.Id,
                    ReleaseDate = um.Movie.ReleaseDate.ToString("MMMM yyyy"),
                })
                .ToListAsync();

            return View(movies);
        }
        public async Task<IActionResult>AddToWatchList(string movieId)
        {
            bool isValidGuid = Guid.TryParse(movieId, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index","Movie");
            }
            string userId = userManager.GetUserId(User)!;

           var userMovie= await context.UsersMovies
                .FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId==guidId);

            if (userMovie == null)
            {
                userMovie = new UserMovie
                {
                    UserId = userId,
                    MovieId = guidId,
                };

                context.UsersMovies.Add(userMovie);

                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromWatchlist(string movieId)
        {

            bool isValidGuid = Guid.TryParse(movieId, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }

            string userId = userManager.GetUserId(User)!;

            var userMovie = await context.UsersMovies
               .FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == guidId);

            if(userMovie != null)
            {
                context.UsersMovies.Remove(userMovie);
                await context.SaveChangesAsync();
            }
           return RedirectToAction("Index");
        }
    }
}
