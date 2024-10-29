using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KlubSportowy.Models.ViewModels;
using KlubSportowy.Models;
using System.Threading.Tasks;
using KlubSportowy.Data;
using Microsoft.EntityFrameworkCore;

namespace KlubSportowy.Areas.Identity.Pages.Ogloszenia
{
    public class OgloszeniaTrenerModel : PageModel
    {
        private readonly AuthDbContext _context;

        public OgloszeniaTrenerModel(AuthDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OgloszeniaViewModel ogloszeniaVM { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Usuwanie przestarza³ych og³oszeñ przy ³adowaniu strony
            await _context.RemoveExpiredOgloszeniaAsync();

            ogloszeniaVM = new OgloszeniaViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Usuwanie przestarza³ych og³oszeñ przy przesy³aniu formularza
            await _context.RemoveExpiredOgloszeniaAsync();

            // Walidacja modelu
            if (!ModelState.IsValid)
                return Page();

            // Tworzenie nowego og³oszenia
            var ogloszenie = new OgloszeniaModel
            {
                Tresc = ogloszeniaVM.Tresc,
                DataUsunieciaOgloszenia = ogloszeniaVM.DataUsunieciaOgloszenia
            };

            // Dodanie og³oszenia do kontekstu
            _context.OgloszeniaModel.Add(ogloszenie);
            await _context.SaveChangesAsync();

            return RedirectToPage("Success"); // Przekierowanie po zapisaniu
        }
    }
}

