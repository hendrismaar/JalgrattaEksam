using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JalgrattaEksamMVC.Data;
using JalgrattaEksamMVC.Models;

namespace JalgrattaEksamMVC.Controllers
{
    public class EksamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EksamsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Teooria()
        {
            var model = _context.Eksam.Where(e => e.Teooria == -1);
            return View(await model.ToListAsync());
        }
        public async Task<IActionResult> Slaalom()
        {
            var model = _context.Eksam.Where(e => e.Teooria == 9 && e.Slaalom == -1);
            return View(await model.ToListAsync());
        }
        public async Task<IActionResult> Ring()
        {
            var model = _context.Eksam.Where(e => e.Teooria == 9 && e.Ringtee == -1);
            return View(await model.ToListAsync());
        }
        public async Task<IActionResult> Tänav()
        {
            var model = _context.Eksam.Where(e => e.Teooria == 9 && e.Tänav == -1);
            return View(await model.ToListAsync());
        }
        public async Task<IActionResult> Luba()
        {
            var model = _context.Eksam
                .Select(e => new LubaViewModel()
                {
                    Id = e.Id,
                    Eesnimi = e.Eesnimi,
                    Perenimi = e.Perenimi,
                    Teooria = e.Teooria,
                    Ringtee = e.Ringtee== -1? ".": e.Ringtee==1 ? "Õnnestus":"Põrus",
                    Slaalom = e.Slaalom == -1 ? "." : e.Slaalom == 1 ? "Õnnestus" : "Põrus",
                    Tänav = e.Tänav == -1 ? "." : e.Tänav == 1 ? "Õnnestus" : "Põrus",
                    Luba = e.Luba == 1? "Väljastatud": e.Tänav == 1? "Väljasta":"."
                });
            return View(await model.ToListAsync());
        }

        public async Task<IActionResult> PassFail(int Id, string Osa, int Tulemus)
        {
            return RedirectToAction(Osa);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeooriaTulemus(int id, [Bind("Id, Teooria")] Eksam tulemus)
        {
            var eksam = await _context.Eksam.FindAsync(tulemus.Id);
            if (eksam == null)
            {
                return NotFound();
            }
            eksam.Teooria = tulemus.Teooria;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eksam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EksamExists(eksam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Teooria));
            }
            return View(eksam);
        }

        // GET: Eksams/Create
        public IActionResult Registreeri()
        {
            return View();
        }

        // POST: Eksams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registreeri([Bind("Id,Eesnimi,Perenimi")] Eksam eksam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eksam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eksam);
        }



        // GET: Eksams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eksam.ToListAsync());
        }

        // GET: Eksams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksam
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eksam == null)
            {
                return NotFound();
            }

            return View(eksam);
        }

        // GET: Eksams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eksams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Tänav,Luba")] Eksam eksam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eksam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eksam);
        }

        // GET: Eksams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksam.FindAsync(id);
            if (eksam == null)
            {
                return NotFound();
            }
            return View(eksam);
        }

        // POST: Eksams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Tänav,Luba")] Eksam eksam)
        {
            if (id != eksam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eksam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EksamExists(eksam.Id))
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
            return View(eksam);
        }

        // GET: Eksams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksam
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eksam == null)
            {
                return NotFound();
            }

            return View(eksam);
        }

        // POST: Eksams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eksam = await _context.Eksam.FindAsync(id);
            _context.Eksam.Remove(eksam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EksamExists(int id)
        {
            return _context.Eksam.Any(e => e.Id == id);
        }
    }
}
