using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FunTask.Datos;
using FunTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FunTask.Controllers
{
    public class ActividadesHijoController : Controller
    {
        private readonly FunTaskerContext _context;

        public ActividadesHijoController(FunTaskerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int hijoId)
        {
            var actividades = await _context.Actividades.Include(a => a.Recompensa).Where(a => a.HijoId == hijoId).ToListAsync();
            return View(actividades);
        }

        [HttpPost]
        public async Task<IActionResult> IncrementarDia(int actividadId)
        {
            var actividad = await _context.Actividades.FindAsync(actividadId);
            if (actividad != null)
            {
                actividad.DiasCompletados++;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { hijoId = actividad.HijoId });
        }

        public async Task<IActionResult> DetalleRecompensa(int recompensaId)
        {
            var recompensa = await _context.Recompensas.FindAsync(recompensaId);
            return View(recompensa);
        }
    }
}
