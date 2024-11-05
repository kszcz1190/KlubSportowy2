using KlubSportowy.Data;
using KlubSportowy.Migrations;
using KlubSportowy.Models;
using KlubSportowy.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KlubSportowy.Controllers
{
    [Authorize]
    public class OgloszeniaZawodnikController : Controller
    {
        private readonly AuthDbContext _context;

        public OgloszeniaZawodnikController(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ogloszenia = await _context.OgloszeniaModel
                .Include(o => o.ZawodnikOgloszenie)
                .ToListAsync();
            return View("Index", ogloszenia);
        }


        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var zawodnikOgloszenie = await _context.ZawodnikOgloszenie
                .FirstOrDefaultAsync(zo => zo.OgloszenieId == id && zo.ZawodnikId == userId);

            if (zawodnikOgloszenie != null)
            {
                zawodnikOgloszenie.IsRead = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Jeśli rekord nie istnieje, dodaj nowy
                zawodnikOgloszenie = new ZawodnikOgloszenie
                {
                    ZawodnikId = userId,
                    OgloszenieId = id,
                    IsRead = true
                };
                _context.ZawodnikOgloszenie.Add(zawodnikOgloszenie);
            }

            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Ogłoszenie zostało oznaczone jako przeczytane.";
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Filter(string filter)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ogloszenia = await _context.OgloszeniaModel
                .Include(o => o.ZawodnikOgloszenie)
                .ToListAsync();

            // Wprowadź logikę filtrowania na podstawie zmiennej "filter"
            if (filter == "Przeczytane")
            {
                ogloszenia = ogloszenia.Where(o => o.ZawodnikOgloszenie.Any(z => z.ZawodnikId == currentUserId && z.IsRead)).ToList();


            }
            if (filter == "Nieprzeczytane")
            {
                ogloszenia = ogloszenia.Where(o => o.ZawodnikOgloszenie.Any(z => z.ZawodnikId == currentUserId && !z.IsRead)).ToList();
            }

            return View("Index", ogloszenia);
        }

    }
}
