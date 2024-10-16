using GameZone.Data;
using GameZone.Data.Models;
using GameZone.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly GameZoneDbContext context;
        private readonly UserManager<IdentityUser> manager;

        public GameController(GameZoneDbContext _context, UserManager<IdentityUser> _manager)
        {
            context= _context;  
            manager= _manager;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var genres= await context.Genres
                .Select(g=>new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToListAsync();

            var model = new GameAddModel
            {
                Genres = genres
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Add(GameAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           string userId=GetUserId();

            string dateTimeString = $"{model.ReleasedOn}";

            bool isValid = DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parseDateTime);

            if (!isValid)
            {
                throw new InvalidOperationException("Invalid date format.");
            }

            Game game = new Game()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                ReleasedOn = parseDateTime,
                PublisherId = userId,
                GenreId = model.GenreId,
            };

            await context.AddAsync(game);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult>All()
        {
            var games= await context
                .Games
                .Where(g=>g.IsDeleted==false)
                .Select(g=>new AllGamesViewModel
                {
                    ImageUrl = g.ImageUrl,
                    Title = g.Title,
                    Genre = g.Genre.Name,
                    ReleasedOn=g.ReleasedOn.ToString(DateFormat),
                    Id=g.Id,
                    Publisher=g.Publisher.UserName,
                })
                .ToListAsync();

            return View(games);
        }

        public async Task<IActionResult>AddToMyZone(int id)
        {
            var game=await context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if(game==null || game.IsDeleted==true)
            {
                return BadRequest();
            }
            

            string userId = GetUserId();

            var gamesCollection=await context.GamersGames
                .Where(gg=>gg.GamerId == userId)
                .ToListAsync();

            if(!gamesCollection.Any(g=>g.GameId==game.Id))
            {
                var GamerGame = new GamerGame
                {
                    GameId = game.Id,
                    GamerId = userId,
                };

                await context.GamersGames.AddAsync(GamerGame);
                await context.SaveChangesAsync();

                
                return RedirectToAction("MyZone"); 
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult>MyZone()
        {

            string userId = GetUserId();


            var games = await context
                .GamersGames
                .Where(g=>g.GamerId == userId && g.Game.IsDeleted==false)
                .Select(g => new AllGamesViewModel
                {
                    ImageUrl = g.Game.ImageUrl,
                    Title = g.Game.Title,
                    Genre = g.Game.Genre.Name,
                    ReleasedOn = g.Game.ReleasedOn.ToString(DateFormat),
                    Id = g.Game.Id,
                    Publisher = g.Game.Publisher.UserName
                })
                .ToListAsync();

            return View(games);
        }

        public async Task<IActionResult>StrikeOut(int id)
        {
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if(game==null || game.IsDeleted==true)
            {
                return BadRequest();
            }

            string userId= GetUserId();


            var GamerGame=await context.GamersGames.FirstOrDefaultAsync(gg=>gg.GameId==game.Id && gg.GamerId==userId);

            if(GamerGame==null)
            {
                return NotFound();
            }

            context.GamersGames.Remove(GamerGame);
            await context.SaveChangesAsync();

            return RedirectToAction("MyZone");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == id);

         
            
            if (game == null || game.IsDeleted == true)
            {
                return BadRequest();
            }
            var genres = await context.Genres
               .Select(g => new GenreViewModel
               {
                   Id = g.Id,
                   Name = g.Name,
               })
               .ToListAsync();



            GameAddModel gm = new GameAddModel
            {
                Description = game.Description,
                Title = game.Title,
                Genres = genres,
                ImageUrl = game.ImageUrl,
                ReleasedOn = game.ReleasedOn.ToString(DateFormat),
                GenreId=game.GenreId,
            };

            return View(gm);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id,GameAddModel model)
        {
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null || game.IsDeleted == true)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if(game.PublisherId!=userId)
            {
                return Unauthorized();
            }

            string dateTimeString = $"{model.ReleasedOn}";

            bool isValid = DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parseDateTime);

            if (!isValid)
            {
                throw new InvalidOperationException("Invalid date format.");
            }

            game.Title= model.Title;
            game.Description= model.Description;
            game.ImageUrl= model.ImageUrl;
            game.ReleasedOn = parseDateTime;
            game.GenreId= model.GenreId;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details(int id)
        {

            var model = await context.Games
                .Where(g => g.Id == id && g.IsDeleted==false)
                .Select(g => new DetailsForGame()
                {
                    Title = g.Title,
                    Description = g.Description,
                    Genre = g.Genre.Name,
                    Publisher = g.Publisher.UserName,
                    Id = g.Id,
                    ReleasedOn = g.ReleasedOn.ToString(DateFormat),
                    ImageUrl = g.ImageUrl
                })
                .FirstOrDefaultAsync();
               
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            var model = await context.Games
                 .Where(g => g.Id == id && g.IsDeleted==false)
                 .Select(g => new DeleteGameModel()
                 {
                     Title = g.Title,    
                     Publisher = g.Publisher.UserName,
                     Id = g.Id,
                 })
                 .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmed(int id,DeleteGameModel model)
        {
            var game=await context.Games.FirstOrDefaultAsync(g=>g.Id==id);

            if (game == null || game.IsDeleted == true  )
            {
                return BadRequest();
            }

            game.IsDeleted = true;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }


        private string GetUserId()
        {
            string userId = string.Empty;

            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return userId;
        }
    }
}
