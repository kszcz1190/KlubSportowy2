using KlubSportowy.Areas.Identity.Data;
using KlubSportowy.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KlubSportowy.Data; // Namespace z kontekstem bazy danych i modelami
using KlubSportowy.Models;
using Microsoft.EntityFrameworkCore; // Namespace z modelem ZawodnikModel
using Microsoft.AspNetCore.Authorization;

namespace KlubSportowy.Controllers
{
    [Authorize]
    public class ProfileRedirectController : Controller
    {
   
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileRedirectController(AuthDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> RedirectToZawodnik()
        {
            // Pobierz bieżącego użytkownika
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // Przekierowanie do strony logowania, jeśli użytkownik nie jest zalogowany
                return RedirectToAction("Login", "Account");
            }

            // Znajdź model ZawodnikModel, który jest powiązany z tym użytkownikiem
            var zawodnik = await _context.ZawodnikModel
                .FirstOrDefaultAsync(z => z.ApplicationUserId == user.Id);

            if (zawodnik != null)
            {
                // Przekierowanie do szczegółów ZawodnikModel z odpowiednim id
                return Redirect($"http://localhost:5001/ZawodnikModels/details/{zawodnik.Id}");
            }

            // W przypadku braku powiązanego ZawodnikModel wyświetl informację lub przekieruj na inną stronę
            return RedirectToAction("NoProfile", "ProfileRedirect");
        }

        // Opcjonalna akcja na wypadek braku przypisanego profilu
        public IActionResult NoProfile()
        {
            return View();
        }
    }
}


