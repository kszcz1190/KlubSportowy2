using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KlubSportowy.Data;
using KlubSportowy.Models;
using Microsoft.AspNetCore.Authorization;

namespace KlubSportowy.Controllers
{
    [Authorize]
    public class ZawodnikModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public ZawodnikModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: ZawodnikModels
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.ZawodnikModel.Include(z => z.ApplicationUser);
            return View(await authDbContext.ToListAsync());
        }

        // GET: ZawodnikModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zawodnikModel = await _context.ZawodnikModel
                .Include(z => z.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zawodnikModel == null)
            {
                return NotFound();
            }

            return View(zawodnikModel);
        }

        // GET: ZawodnikModels/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ZawodnikModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Wiek,Kraj,Pozycja,LacznaIloscGoli,LacznaIloscZoltychKartek,LacznaIloscCzerwonychKartek,LacznaIloscMeczyRozegranych,LacznaIloscMinutRozegranych,NumerZawodnika,ApplicationUserId")] ZawodnikModel zawodnikModel)
        {
            //if (ModelState.IsValid)
            //{
              _context.Add(zawodnikModel);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
            //}
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", zawodnikModel.ApplicationUserId);
            //return View(zawodnikModel);
        }

        // GET: ZawodnikModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zawodnikModel = await _context.ZawodnikModel.FindAsync(id);
            if (zawodnikModel == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", zawodnikModel.ApplicationUserId);
            return View(zawodnikModel);
        }

        // POST: ZawodnikModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Wiek,Kraj,Pozycja,LacznaIloscGoli,LacznaIloscZoltychKartek,LacznaIloscCzerwonychKartek,LacznaIloscMeczyRozegranych,LacznaIloscMinutRozegranych,NumerZawodnika,ApplicationUserId")] ZawodnikModel zawodnikModel)
        {
            if (id != zawodnikModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zawodnikModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZawodnikModelExists(zawodnikModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", zawodnikModel.ApplicationUserId);
            return View(zawodnikModel);
        }

        // GET: ZawodnikModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zawodnikModel = await _context.ZawodnikModel
                .Include(z => z.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zawodnikModel == null)
            {
                return NotFound();
            }

            return View(zawodnikModel);
        }

        // POST: ZawodnikModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zawodnikModel = await _context.ZawodnikModel.FindAsync(id);
            if (zawodnikModel != null)
            {
                _context.ZawodnikModel.Remove(zawodnikModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZawodnikModelExists(int id)
        {
            return _context.ZawodnikModel.Any(e => e.Id == id);
        }
    }
}
