using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FunTask.Datos;
using FunTask.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FunTask.Controllers
{
    [Authorize]
    public class ActividadesController : Controller
    {
        private readonly FunTaskerContext _context;

        public ActividadesController(FunTaskerContext context)
        {
            _context = context;
        }

        // GET: Actividades
        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario actual
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtrar actividades relacionadas al usuario correspondiente
            var actividades = _context.Actividades
                .Include(a => a.Hijo)
                .Include(a => a.Recompensa)
                .Where(a => a.Hijo.UsuarioId == userId);

            return View(await actividades.ToListAsync());
        }

        // GET: Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades
                .Include(a => a.Hijo)
                .Include(a => a.Recompensa)
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // GET: Actividades/Create
        public IActionResult Create()
        {
            // Obtener el ID del usuario actual
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtrar hijos relacionados al usuario correspondiente
            var hijos = _context.Hijos.Where(h => h.UsuarioId == userId);
            ViewData["HijoId"] = new SelectList(hijos, "HijoId", "HijoId");
            ViewData["RecompensaId"] = new SelectList(_context.Recompensas, "RecompensaId", "Nombre");
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActividadId,HijoId,Titulo,Imagen,Descripcion,DiasParaRecompensa,DiasCompletados,RecompensaId")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actividad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HijoId"] = new SelectList(_context.Hijos, "HijoId", "HijoId", actividad.HijoId);
            ViewData["RecompensaId"] = new SelectList(_context.Recompensas, "RecompensaId", "Nombre", actividad.RecompensaId);
            return View(actividad);
        }

        // GET: Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hijos = _context.Hijos.Where(h => h.UsuarioId == userId);
            if (hijos == null || hijos.Count() == 0)
            {
                return NotFound();
            }
            ViewData["HijoId"] = new SelectList(hijos, "HijoId", "HijoId");
            ViewData["RecompensaId"] = new SelectList(_context.Recompensas, "RecompensaId", "Nombre", actividad.RecompensaId);
            return View(actividad);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActividadId,HijoId,Titulo,Imagen,Descripcion,DiasParaRecompensa,DiasCompletados,RecompensaId")] Actividad actividad)
        {
            if (id != actividad.ActividadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActividadExists(actividad.ActividadId))
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
            ViewData["HijoId"] = new SelectList(_context.Hijos, "HijoId", "HijoId", actividad.HijoId);
            ViewData["RecompensaId"] = new SelectList(_context.Recompensas, "RecompensaId", "Nombre", actividad.RecompensaId);
            return View(actividad);
        }

        // GET: Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hijos = _context.Hijos.Where(h => h.UsuarioId == userId);
            if (hijos == null || hijos.Count()== 0)
            {
                return NotFound();
            }
            ViewData["HijoId"] = new SelectList(hijos, "HijoId", "HijoId");

            var actividad = await _context.Actividades
                .Include(a => a.Hijo)
                .Include(a => a.Recompensa)
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actividades == null)
            {
                return Problem("Entity set 'FunTaskerContext.Actividades'  is null.");
            }
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad != null)
            {
                _context.Actividades.Remove(actividad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActividadExists(int id)
        {
          return (_context.Actividades?.Any(e => e.ActividadId == id)).GetValueOrDefault();
        }
    }
}
