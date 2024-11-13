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
    public class PrzedmiotModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public PrzedmiotModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: PrzedmiotModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrzedmiotModel.ToListAsync());
        }

        // GET: PrzedmiotModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiotModel = await _context.PrzedmiotModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiotModel == null)
            {
                return NotFound();
            }

            return View(przedmiotModel);
        }

        // GET: PrzedmiotModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrzedmiotModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CenaPrzedmiotu,IloscPrzedmiotu,NazwaPrzedmiotu,OpisPrzedmiotu,CzyDostepny,ZdjecieUrl")] PrzedmiotModel przedmiotModel)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(przedmiotModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(przedmiotModel);
        }

        // GET: PrzedmiotModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiotModel = await _context.PrzedmiotModel.FindAsync(id);
            if (przedmiotModel == null)
            {
                return NotFound();
            }
            return View(przedmiotModel);
        }

        // POST: PrzedmiotModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CenaPrzedmiotu,IloscPrzedmiotu,NazwaPrzedmiotu,OpisPrzedmiotu,CzyDostepny,ZdjecieUrl")] PrzedmiotModel przedmiotModel)
        {
            if (id != przedmiotModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiotModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotModelExists(przedmiotModel.Id))
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
            return View(przedmiotModel);
        }

        // GET: PrzedmiotModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiotModel = await _context.PrzedmiotModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiotModel == null)
            {
                return NotFound();
            }

            return View(przedmiotModel);
        }

        // POST: PrzedmiotModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przedmiotModel = await _context.PrzedmiotModel.FindAsync(id);
            if (przedmiotModel != null)
            {
                _context.PrzedmiotModel.Remove(przedmiotModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotModelExists(int id)
        {
            return _context.PrzedmiotModel.Any(e => e.Id == id);
        }
    }
}
