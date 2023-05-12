using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FunTask.Datos;
using FunTask.Models;

namespace FunTask.Controllers
{
    public class RecompensasController : Controller
    {
        private readonly FunTaskerContext _context;

        public RecompensasController(FunTaskerContext context)
        {
            _context = context;
        }

        // GET: Recompensas
        public async Task<IActionResult> Index()
        {
              return _context.Recompensas != null ? 
                          View(await _context.Recompensas.ToListAsync()) :
                          Problem("Entity set 'FunTaskerContext.Recompensas'  is null.");
        }

        // GET: Recompensas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recompensas == null)
            {
                return NotFound();
            }

            var recompensa = await _context.Recompensas
                .FirstOrDefaultAsync(m => m.RecompensaId == id);
            if (recompensa == null)
            {
                return NotFound();
            }

            return View(recompensa);
        }

        // GET: Recompensas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recompensas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecompensaId,Nombre,Imagen,Descripcion")] Recompensa recompensa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recompensa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recompensa);
        }

        // GET: Recompensas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recompensas == null)
            {
                return NotFound();
            }

            var recompensa = await _context.Recompensas.FindAsync(id);
            if (recompensa == null)
            {
                return NotFound();
            }
            return View(recompensa);
        }

        // POST: Recompensas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecompensaId,Nombre,Imagen,Descripcion")] Recompensa recompensa)
        {
            if (id != recompensa.RecompensaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recompensa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecompensaExists(recompensa.RecompensaId))
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
            return View(recompensa);
        }

        // GET: Recompensas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recompensas == null)
            {
                return NotFound();
            }

            var recompensa = await _context.Recompensas
                .FirstOrDefaultAsync(m => m.RecompensaId == id);
            if (recompensa == null)
            {
                return NotFound();
            }

            return View(recompensa);
        }

        // POST: Recompensas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recompensas == null)
            {
                return Problem("Entity set 'FunTaskerContext.Recompensas'  is null.");
            }
            var recompensa = await _context.Recompensas.FindAsync(id);
            if (recompensa != null)
            {
                _context.Recompensas.Remove(recompensa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecompensaExists(int id)
        {
          return (_context.Recompensas?.Any(e => e.RecompensaId == id)).GetValueOrDefault();
        }
    }
}
