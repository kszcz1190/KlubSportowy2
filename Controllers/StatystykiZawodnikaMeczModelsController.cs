using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KlubSportowy.Data;
using KlubSportowy.Models;

namespace KlubSportowy.Controllers
{
    public class StatystykiZawodnikaMeczModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public StatystykiZawodnikaMeczModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: StatystykiZawodnikaMeczModels
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.StatystykiZawodnikaMeczModel.Include(s => s.MeczModel).Include(s => s.ZawodnikModel);
            return View(await authDbContext.ToListAsync());
        }

        // GET: StatystykiZawodnikaMeczModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statystykiZawodnikaMeczModel = await _context.StatystykiZawodnikaMeczModel
                .Include(s => s.MeczModel)
                .Include(s => s.ZawodnikModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statystykiZawodnikaMeczModel == null)
            {
                return NotFound();
            }

            return View(statystykiZawodnikaMeczModel);
        }

        // GET: StatystykiZawodnikaMeczModels/Create
        public IActionResult Create()
        {
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna");
            ViewData["ZawodnikModelId"] = new SelectList(_context.ZawodnikModel, "Id", "Id");
            return View();
        }

        // POST: StatystykiZawodnikaMeczModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeczModelId,ZawodnikModelId,IloscGoli,IloscZoltychKartek,IloscCzerwonychKartek,IloscMinutRozegranych,CzyZawodnikZagralWMeczu,CzyKapitan,Pozycja")] StatystykiZawodnikaMeczModel statystykiZawodnikaMeczModel)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(statystykiZawodnikaMeczModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", statystykiZawodnikaMeczModel.MeczModelId);
            //ViewData["ZawodnikModelId"] = new SelectList(_context.ZawodnikModel, "Id", "Id", statystykiZawodnikaMeczModel.ZawodnikModelId);
            //return View(statystykiZawodnikaMeczModel);
        }

        // GET: StatystykiZawodnikaMeczModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statystykiZawodnikaMeczModel = await _context.StatystykiZawodnikaMeczModel.FindAsync(id);
            if (statystykiZawodnikaMeczModel == null)
            {
                return NotFound();
            }
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", statystykiZawodnikaMeczModel.MeczModelId);
            ViewData["ZawodnikModelId"] = new SelectList(_context.ZawodnikModel, "Id", "Id", statystykiZawodnikaMeczModel.ZawodnikModelId);
            return View(statystykiZawodnikaMeczModel);
        }

        // POST: StatystykiZawodnikaMeczModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeczModelId,ZawodnikModelId,IloscGoli,IloscZoltychKartek,IloscCzerwonychKartek,IloscMinutRozegranych,CzyZawodnikZagralWMeczu,CzyKapitan,Pozycja")] StatystykiZawodnikaMeczModel statystykiZawodnikaMeczModel)
        {
            if (id != statystykiZawodnikaMeczModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statystykiZawodnikaMeczModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatystykiZawodnikaMeczModelExists(statystykiZawodnikaMeczModel.Id))
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
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", statystykiZawodnikaMeczModel.MeczModelId);
            ViewData["ZawodnikModelId"] = new SelectList(_context.ZawodnikModel, "Id", "Id", statystykiZawodnikaMeczModel.ZawodnikModelId);
            return View(statystykiZawodnikaMeczModel);
        }

        // GET: StatystykiZawodnikaMeczModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statystykiZawodnikaMeczModel = await _context.StatystykiZawodnikaMeczModel
                .Include(s => s.MeczModel)
                .Include(s => s.ZawodnikModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statystykiZawodnikaMeczModel == null)
            {
                return NotFound();
            }

            return View(statystykiZawodnikaMeczModel);
        }

        // POST: StatystykiZawodnikaMeczModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statystykiZawodnikaMeczModel = await _context.StatystykiZawodnikaMeczModel.FindAsync(id);
            if (statystykiZawodnikaMeczModel != null)
            {
                _context.StatystykiZawodnikaMeczModel.Remove(statystykiZawodnikaMeczModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatystykiZawodnikaMeczModelExists(int id)
        {
            return _context.StatystykiZawodnikaMeczModel.Any(e => e.Id == id);
        }
    }
}
