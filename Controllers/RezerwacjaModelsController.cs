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
    public class RezerwacjaModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public RezerwacjaModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: RezerwacjaModels
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.RezerwacjaModel.Include(r => r.applicationUser).Include(r => r.przedmiotModel);
            return View(await authDbContext.ToListAsync());
        }

        // GET: RezerwacjaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerwacjaModel = await _context.RezerwacjaModel
                .Include(r => r.applicationUser)
                .Include(r => r.przedmiotModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacjaModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjaModel);
        }

        // GET: RezerwacjaModels/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PrzedmiotModelId"] = new SelectList(_context.PrzedmiotModel, "Id", "Id");
            return View();
        }

        // POST: RezerwacjaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,PrzedmiotModelId")] RezerwacjaModel rezerwacjaModel)
        {
            //if (ModelState.IsValid)
            //
            _context.Add(rezerwacjaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", rezerwacjaModel.ApplicationUserId);
            //ViewData["PrzedmiotModelId"] = new SelectList(_context.PrzedmiotModel, "Id", "Id", rezerwacjaModel.PrzedmiotModelId);
            //return View(rezerwacjaModel);
        }

        // GET: RezerwacjaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerwacjaModel = await _context.RezerwacjaModel.FindAsync(id);
            if (rezerwacjaModel == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", rezerwacjaModel.ApplicationUserId);
            ViewData["PrzedmiotModelId"] = new SelectList(_context.PrzedmiotModel, "Id", "Id", rezerwacjaModel.PrzedmiotModelId);
            return View(rezerwacjaModel);
        }

        // POST: RezerwacjaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,PrzedmiotModelId")] RezerwacjaModel rezerwacjaModel)
        {
            if (id != rezerwacjaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezerwacjaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezerwacjaModelExists(rezerwacjaModel.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", rezerwacjaModel.ApplicationUserId);
            ViewData["PrzedmiotModelId"] = new SelectList(_context.PrzedmiotModel, "Id", "Id", rezerwacjaModel.PrzedmiotModelId);
            return View(rezerwacjaModel);
        }

        // GET: RezerwacjaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerwacjaModel = await _context.RezerwacjaModel
                .Include(r => r.applicationUser)
                .Include(r => r.przedmiotModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacjaModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjaModel);
        }

        // POST: RezerwacjaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezerwacjaModel = await _context.RezerwacjaModel.FindAsync(id);
            if (rezerwacjaModel != null)
            {
                _context.RezerwacjaModel.Remove(rezerwacjaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjaModelExists(int id)
        {
            return _context.RezerwacjaModel.Any(e => e.Id == id);
        }
    }
}
