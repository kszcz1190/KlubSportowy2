using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KlubSportowy.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KlubSportowy.Controllers
{
    public class KalendarzController : Controller
    {
        private readonly AuthDbContext _context;

        public KalendarzController(AuthDbContext context)
        {
            _context = context;
        }

        // Akcja do pobierania wydarzeń w formacie JSON
        public async Task<IActionResult> GetWydarzenia()
        {
            var wydarzenia = await _context.WydarzenieModel.Select(w => new
            {
                title = w.NazwaWydarzenia,
                start = w.DataWydarzenia.ToString("yyyy-MM-dd"),
                description = w.OpisWydarzenia
            }).ToListAsync();

            return Json(wydarzenia);
        }

        // Inne akcje, np. Index
        public IActionResult Index()
        {
            return View();
        }
    }
}

