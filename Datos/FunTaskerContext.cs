using FunTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FunTask.Datos
{
    public class FunTaskerContext : IdentityDbContext
    {
        public FunTaskerContext(DbContextOptions<FunTaskerContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Hijo> Hijos { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Hijos)
                .WithOne(h => h.Usuario)
                .HasForeignKey(h => h.UsuarioId);
        }
    }
}

