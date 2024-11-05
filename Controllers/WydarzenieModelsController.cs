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
    public class WydarzenieModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public WydarzenieModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: WydarzenieModels
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.WydarzenieModel.Include(w => w.MeczModel);
            return View(await authDbContext.ToListAsync());
        }

        // GET: WydarzenieModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydarzenieModel = await _context.WydarzenieModel
                .Include(w => w.MeczModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wydarzenieModel == null)
            {
                return NotFound();
            }

            return View(wydarzenieModel);
        }

        // GET: WydarzenieModels/Create
        public IActionResult Create()
        {
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna");
            return View();
        }

        // POST: WydarzenieModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataWydarzenia,TypWydarzenia,OpisWydarzenia,MiejsceWydarzenia,NazwaWydarzenia,MeczModelId")] WydarzenieModel wydarzenieModel)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(wydarzenieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", wydarzenieModel.MeczModelId);
            //return View(wydarzenieModel);
        }

        // GET: WydarzenieModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydarzenieModel = await _context.WydarzenieModel.FindAsync(id);
            if (wydarzenieModel == null)
            {
                return NotFound();
            }
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", wydarzenieModel.MeczModelId);
            return View(wydarzenieModel);
        }

        // POST: WydarzenieModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataWydarzenia,TypWydarzenia,OpisWydarzenia,MiejsceWydarzenia,NazwaWydarzenia,MeczModelId")] WydarzenieModel wydarzenieModel)
        {
            if (id != wydarzenieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wydarzenieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WydarzenieModelExists(wydarzenieModel.Id))
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
            ViewData["MeczModelId"] = new SelectList(_context.MeczModel, "Id", "Druzyna", wydarzenieModel.MeczModelId);
            return View(wydarzenieModel);
        }

        // GET: WydarzenieModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydarzenieModel = await _context.WydarzenieModel
                .Include(w => w.MeczModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wydarzenieModel == null)
            {
                return NotFound();
            }

            return View(wydarzenieModel);
        }

        // POST: WydarzenieModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wydarzenieModel = await _context.WydarzenieModel.FindAsync(id);
            if (wydarzenieModel != null)
            {
                _context.WydarzenieModel.Remove(wydarzenieModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WydarzenieModelExists(int id)
        {
            return _context.WydarzenieModel.Any(e => e.Id == id);
        }
    }
}
