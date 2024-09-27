using CinemaApp.Data;
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
    }
}
