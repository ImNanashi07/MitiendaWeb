using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaWeb.Data;
using MiTiendaWeb.Models;
using System.Diagnostics;

namespace MiTiendaWeb.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var cervezas = _context.Cerveza.Include(c => c.Estilo);
            return View(); //await cervezas.ToListAsync()); <= me daba un punto de interrupcion cuando lo ponia, no se porque, asi que lo deje sin el await y sin el ToListAsync() y funciona, pero no se si es correcto
        }

        public async Task<IActionResult> Detalles(int? id) // no me dejo crear la vista de detalles, lo intente 3 veces y no la agrego.
        {
            var cerveza = _context.Cerveza.Include(c => c.Estilo).FirstOrDefaultAsync(c => c.id == id);
            return View(cerveza);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
