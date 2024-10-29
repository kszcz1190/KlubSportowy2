using KlubSportowy.Data;
using KlubSportowy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KlubSportowy.Controllers
{
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Pobierz identyfikator aktualnego użytkownika
            var zawodnikOgloszenie = await _context.ZawodnikOgloszenie
                .FirstOrDefaultAsync(zo => zo.OgloszenieId == id && zo.ZawodnikId == userId);

            if (zawodnikOgloszenie != null)
            {
                zawodnikOgloszenie.IsRead = true; // Zmiana statusu na przeczytane
                await _context.SaveChangesAsync(); // Zapisz zmiany w bazie danych
            }

            return RedirectToAction("Index"); // Przekieruj do metody Index
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
            else if (filter == "Nieprzeczytane")
            {
                ogloszenia = ogloszenia.Where(o => o.ZawodnikOgloszenie.Any(z => z.ZawodnikId == currentUserId && !z.IsRead)).ToList();
            }

            return View("Index", ogloszenia);
        }
    }
}
