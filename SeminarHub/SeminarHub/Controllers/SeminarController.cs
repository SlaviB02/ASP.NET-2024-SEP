using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Common.ValidationConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> All()
        {
            var seminars =  await context.Seminars
                .Select(s => new SeminarAllViewModel
                {
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
                    Id = s.Id,
                    Organizer = s.Organizer.UserName,
                })
                .ToListAsync();

            return View(seminars);
        }
        [HttpGet]
        public async Task<IActionResult>Add()
        {
            var model = new SeminarAddModel();

            var categories = await GetCategories();

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarAddModel model)
        {

            if(ModelState.IsValid==false)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            DateTime DateOfSeminar;

            bool isValid = DateTime.TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture,DateTimeStyles.None,out DateOfSeminar);

            if (!isValid)
            {
                ModelState.AddModelError("DateAndTime", "Invalid format");
                model.Categories = await GetCategories();
                return View(model);
            }
            string userId=GetUserId();
            Seminar seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = DateOfSeminar,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId=userId,
            };


            await context.Seminars.AddAsync(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult>Joined()
        {
            string userId = GetUserId();

            var seminars = await context.SeminarsParticipants
                .Where(p => p.ParticipantId == userId)
               .Include(s => s.Seminar)
                .Select(s => new SeminarAllViewModel
                {
                    Topic = s.Seminar.Topic,
                    Lecturer = s.Seminar.Lecturer,
                    Category = s.Seminar.Category.Name,
                    DateAndTime = s.Seminar.DateAndTime.ToString(DateTimeFormat),
                    Id = s.Seminar.Id,
                    Organizer = s.Seminar.Organizer.UserName,
                })
                .ToListAsync();

            return View(seminars);
        }
        public async Task<IActionResult>Join(int id)
        {
            string userId = GetUserId();

            var seminar=await context.Seminars.FirstOrDefaultAsync(s => s.Id == id);

            if(seminar == null)
            {
                return RedirectToAction("All");
            }

            if(seminar.OrganizerId==userId)
            {
                return RedirectToAction("All");
            }

            var seminarp = await context.SeminarsParticipants.FirstOrDefaultAsync(s => s.ParticipantId == userId && s.SeminarId == id);

            if (seminarp!=null)
            {
                return RedirectToAction("All");

            }
            SeminarParticipant sp = new SeminarParticipant()
            {
                ParticipantId = userId,
                SeminarId = id,
            };
            await context.SeminarsParticipants.AddAsync(sp);
            await context.SaveChangesAsync();

            return RedirectToAction("Joined");
        }

        public async Task<IActionResult>Leave(int id)
        {
            string userId = GetUserId();

            var seminar = await context.Seminars.FirstOrDefaultAsync(s => s.Id == id);

            if (seminar == null)
            {
                return RedirectToAction("All");
            }

            if (seminar.OrganizerId==userId)
            {
                return RedirectToAction("All");
            }

            var sp = await context.SeminarsParticipants.FirstOrDefaultAsync(s => s.ParticipantId == userId && s.SeminarId == id);

            if (sp==null)
            {
                return RedirectToAction("All");
               
            }
            context.SeminarsParticipants.Remove(sp);
            await context.SaveChangesAsync();

            return RedirectToAction("Joined");

        }
        [HttpGet]
        public async Task<IActionResult>Edit(int id)
        {

            string userId = GetUserId();

            var seminarOrganizerId = await context.Seminars.Where(s => s.Id == id).Select(s => s.OrganizerId).FirstOrDefaultAsync();

            if(seminarOrganizerId!=userId)
            {
                return RedirectToAction("All");
            }

            var categories=await GetCategories();
            var seminar = await context.Seminars
                .Where(s=>s.Id==id)
                .Select(s => new SeminarAddModel()
                {

                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
                    Duration = s.Duration,
                    CategoryId = s.CategoryId,
                    Categories=categories
                })
                .FirstOrDefaultAsync();



            return View(seminar);
                
                
        }
        [HttpPost]

        public async Task<IActionResult>Edit(SeminarAddModel model,int id)
        {
            var seminar = await context.Seminars.FirstOrDefaultAsync(s => s.Id == id);

            string userId = GetUserId();    

            if (seminar==null || seminar.OrganizerId!=userId)
            {
                return RedirectToAction("All");
            }

            DateTime DateOfSeminar;

            bool isValid = DateTime.TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOfSeminar);

            if (!isValid)
            {

                return RedirectToAction("Edit");
            }

            seminar.Topic= model.Topic;
            seminar.Lecturer= model.Lecturer;
            seminar.Details= model.Details;
            seminar.DateAndTime = DateOfSeminar;
            seminar.Duration = model.Duration;
            seminar.CategoryId= model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var model=await context.Seminars
                .Where(s=>s.Id == id)
                .Select(s=>new SeminarDetailsView()
                {
                    Topic= s.Topic,
                    DateAndTime=s.DateAndTime.ToString(DateTimeFormat),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Details= s.Details,
                    Category=s.Category.Name,
                    Organizer=s.Organizer.UserName,
                    Id=id
                })
                .FirstOrDefaultAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult>Delete(int Id)
        {

            string userId=GetUserId();
            var seminarOrganizerId = await context.Seminars.Where(s => s.Id == Id).Select(s => s.OrganizerId).FirstOrDefaultAsync();

            if (seminarOrganizerId != userId)
            {
                return RedirectToAction("All");
            }

            var seminar=await context.Seminars
                .Where(s=> s.Id == Id)
                .Select(s=>new DeleteViewModelSeminar()
                {
                    DateAndTime= s.DateAndTime,
                    Topic= s.Topic,
                })
                .FirstOrDefaultAsync();

            return View(seminar);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteViewModelSeminar model)
        {
            var seminar = await context.Seminars.FindAsync(model.Id);

            string userId=GetUserId();  
            if (seminar == null || seminar.OrganizerId!=userId)
            {
                return RedirectToAction("All");
            }

            context.Seminars.Remove(seminar);
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
        private async Task<List<CategoryViewModel>>GetCategories()
        {
            var categories = await context.Categories
               .Select(c => new CategoryViewModel()
               {
                   Id = c.Id,
                   Name = c.Name,
               })
               .ToListAsync();
            return categories;
        }
    }
}
