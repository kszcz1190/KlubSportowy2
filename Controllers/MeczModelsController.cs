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
    public class MeczModelsController : Controller
    {
        private readonly AuthDbContext _context;

        public MeczModelsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: MeczModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeczModel.ToListAsync());
        }

        // GET: MeczModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meczModel = await _context.MeczModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meczModel == null)
            {
                return NotFound();
            }

            return View(meczModel);
        }

        // GET: MeczModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeczModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Druzyna,DruzynaPrzeciwna,GoleDruzyna,GoleDruzynaPrzeciwna,IloscZoltychKartekDruzyna,IloscZoltychKartekDruzynaPrzeciwna,IloscCzerwonychKartekDruzyna,IloscCzerwonychKartekDruzynaPrzeciwna")] MeczModel meczModel)
        {
            //if (ModelState.IsValid)
            //{
               _context.Add(meczModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //return View(meczModel);
        }

        // GET: MeczModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meczModel = await _context.MeczModel.FindAsync(id);
            if (meczModel == null)
            {
                return NotFound();
            }
            return View(meczModel);
        }

        // POST: MeczModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Druzyna,DruzynaPrzeciwna,GoleDruzyna,GoleDruzynaPrzeciwna,IloscZoltychKartekDruzyna,IloscZoltychKartekDruzynaPrzeciwna,IloscCzerwonychKartekDruzyna,IloscCzerwonychKartekDruzynaPrzeciwna")] MeczModel meczModel)
        {
            if (id != meczModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meczModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeczModelExists(meczModel.Id))
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
            return View(meczModel);
        }

        // GET: MeczModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meczModel = await _context.MeczModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meczModel == null)
            {
                return NotFound();
            }

            return View(meczModel);
        }

        // POST: MeczModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meczModel = await _context.MeczModel.FindAsync(id);
            if (meczModel != null)
            {
                _context.MeczModel.Remove(meczModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeczModelExists(int id)
        {
            return _context.MeczModel.Any(e => e.Id == id);
        }
    }
}
