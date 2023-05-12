using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FunTask.Datos;
using FunTask.Models;
using Microsoft.EntityFrameworkCore;

namespace FunTask.Controllers
{
    [Authorize]
    public class SeleccionPerfilController : Controller
    {
        private readonly FunTaskerContext _context;

        public SeleccionPerfilController(FunTaskerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
      
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hijos = await _context.Hijos.Where(h => h.UsuarioId == userId).ToListAsync();
            return View(hijos);
        }
    }
}
