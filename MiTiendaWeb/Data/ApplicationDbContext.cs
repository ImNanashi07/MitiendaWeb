using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiTiendaWeb.Models;

namespace MiTiendaWeb.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
    
        public DbSet<MiTiendaWeb.Models.Cerveza> Cerveza { get; set; } = default!;
    
        public DbSet<MiTiendaWeb.Models.Estilo> Estilo { get; set; } = default!;
    
        public Dbset<Estilo> Estilos { get; set; }
        public Dbset<Cerveza> cervezas { get; set; }

    }
}
