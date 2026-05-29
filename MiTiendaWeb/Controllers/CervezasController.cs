
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaWeb.Models;
using MiTiendaWeb.Data;

public class CervezasController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnviroment;

    public CervezasController(ApplicationDbContext context, IWebHostEnvironment hostEnviroment)
    {
        _context = context;
        _hostEnviroment = hostEnviroment;
    }

    // GET: CERVEZAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cerveza.ToListAsync());
    }

    // GET: CERVEZAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cerveza = await _context.Cerveza
            .FirstOrDefaultAsync(m => m.id == id);
        if (cerveza == null)
        {
            return NotFound();
        }

        return View(cerveza);
    }

    // GET: CERVEZAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CERVEZAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,nombre,alcohol,idEstilo,Estilo,precio, urlImagen")] Cerveza cerveza)
    {
        if (ModelState.IsValid)
        {
            string rutaPrincipal = _hostEnviroment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;
            if (archivos.Count > 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\cervezas");
                var extension = Path.GetExtension(archivos[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStream);
                }
                cerveza.urlImagen = @"imagenes\cervezas\" + nombreArchivo + extension;
            }
            _context.Add(cerveza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cerveza);
    }

    // GET: CERVEZAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cerveza = await _context.Cerveza.FindAsync(id);
        if (cerveza == null)
        {
            return NotFound();
        }
        return View(cerveza);
    }

    // POST: CERVEZAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,nombre,alcohol,idEstilo,Estilo,precio, urlImagen")] Cerveza cerveza)
    {
        if (id != cerveza.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                string rutaPrincipal = _hostEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (archivos.Count > 0)
                {
                    Cerveza? cervezabd = await _context.Cerveza.FindAsync(id);
                    if(cervezabd != null && cervezabd.urlImagen != null)
                    {
                        var rutaImagenActual = Path.Combine(rutaPrincipal, cervezabd.urlImagen);
                        if(System.IO.File.Exists(rutaImagenActual))
                        {
                            System.IO.File.Delete(rutaImagenActual);
                        }
                        _context.Entry(cervezabd).State = EntityState.Detached;
                    }
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\cervezas");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    cerveza.urlImagen = @"imagenes\cervezas\" + nombreArchivo + extension;
                    _context.Entry(cerveza).State = EntityState.Modified;

                }
                    _context.Update(cerveza);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CervezaExists(cerveza.id))
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
        return View(cerveza);
    }

    // GET: CERVEZAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cerveza = await _context.Cerveza
            .FirstOrDefaultAsync(m => m.id == id);
        if (cerveza == null)
        {
            return NotFound();
        }

        return View(cerveza);
    }

    // POST: CERVEZAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cerveza = await _context.Cerveza.FindAsync(id);
        if (cerveza != null)
        {
            _context.Cerveza.Remove(cerveza);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CervezaExists(int? id)
    {
        return _context.Cerveza.Any(e => e.id == id);
    }
}
