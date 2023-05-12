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
    public class HijosController : Controller
    {
        private readonly FunTaskerContext _context;

        public HijosController(FunTaskerContext context)
        {
            _context = context;
        }

        // GET: Hijos
        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario actual
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtrar hijos relacionados al usuario correspondiente
            var hijos = _context.Hijos.Include(h => h.Usuario).Where(h => h.UsuarioId == userId);

            return View(await hijos.ToListAsync());
        }


        // GET: Hijos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hijos == null)
            {
                return NotFound();
            }

            var hijo = await _context.Hijos
                .Include(h => h.Usuario)
                .FirstOrDefaultAsync(m => m.HijoId == id);
            if (hijo == null)
            {
                return NotFound();
            }

            return View(hijo);
        }

        // GET: Hijos/Create
        public IActionResult Create()
        {
            // No es necesario mostrar una lista de usuarios, ya que el hijo se asignará automáticamente al usuario actual
            return View();
        }

        // POST: Hijos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HijoId,UsuarioId,NombreHijo,ImagenPerfil")] Hijo hijo)
        {
            // Asignar el ID del usuario actual al hijo
            hijo.UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("UsuarioId");
            if (ModelState.IsValid)
            {
                _context.Add(hijo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // No es necesario establecer ViewData["UsuarioId"], ya que el hijo se asigna automáticamente al usuario actual
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", hijo.UsuarioId);
            return View(hijo);
        }

        // GET: Hijos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hijos == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hijo = await _context.Hijos.Where(h => h.UsuarioId == userId).FirstOrDefaultAsync(h => h.HijoId == id);

            if (hijo == null)
            {
                return NotFound();
            }

            return View(hijo);
        }


        // POST: Hijos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HijoId,UsuarioId,NombreHijo,ImagenPerfil")] Hijo hijo)
        {
            if (id != hijo.HijoId)
            {
                return NotFound();
            }
            hijo.UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("UsuarioId");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hijo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HijoExists(hijo.HijoId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", hijo.UsuarioId);
            return View(hijo);
        }

        // GET: Hijos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hijos == null)
            {
                return NotFound();
            }
            /*
            var hijo = await _context.Hijos
                .Include(h => h.Usuario)
                .FirstOrDefaultAsync(m => m.HijoId == id);
            */
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hijo = await _context.Hijos.Where(h => h.UsuarioId == userId).FirstOrDefaultAsync(h => h.HijoId == id);

            if (hijo == null)
            {
                return NotFound();
            }

            return View(hijo);
        }

        // POST: Hijos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hijos == null)
            {
                return Problem("Entity set 'FunTaskerContext.Hijos'  is null.");
            }
            var hijo = await _context.Hijos.FindAsync(id);
            if (hijo != null)
            {
                _context.Hijos.Remove(hijo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HijoExists(int id)
        {
          return (_context.Hijos?.Any(e => e.HijoId == id)).GetValueOrDefault();
        }
    }
}
