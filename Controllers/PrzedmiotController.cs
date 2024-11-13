using KlubSportowy.Data;
using Microsoft.AspNetCore.Mvc;

public class PrzedmiotController : Controller
{
    private readonly AuthDbContext _context;

    public PrzedmiotController(AuthDbContext context)
    {
        _context = context;
    }

    public IActionResult ListaPrzedmiotow()
    {
        var items = _context.PrzedmiotModel.ToList();
        return View(items);
    }
}

