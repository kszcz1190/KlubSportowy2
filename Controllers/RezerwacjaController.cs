using KlubSportowy.Data;
using KlubSportowy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

public class RezerwacjaController : Controller
{
    private readonly AuthDbContext _context;

    public RezerwacjaController(AuthDbContext context)
    {
        _context = context;
    }

    public IActionResult Rezerwacja()
    {
        ViewBag.Przedmioty = _context.PrzedmiotModel
            .Where(p => p.CzyDostepny)
            .Select(p => new { p.Id, p.NazwaPrzedmiotu })
            .ToList();

        var rezerwacja = new RezerwacjaModel
        {
            ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };

        return View(rezerwacja);
    }

    [HttpPost]
    public IActionResult Rezerwuj(RezerwacjaModel model)
    {
        //if (ModelState.IsValid)
        //{
            // Save the reservation
        _context.RezerwacjaModel.Add(model);
        _context.SaveChanges();
        return RedirectToAction("Potwierdzenie"); // Redirect to a confirmation view
        //}

        // Reload items if model state is invalid
        //ViewBag.Przedmioty = _context.PrzedmiotModel
            //.Where(p => p.CzyDostepny)
            //.Select(p => new { p.Id, p.NazwaPrzedmiotu })
            //.ToList();

        //return View("Rezerwacja", model);
    }
}
