using FODUN.Reservas.Models;
using FODUN.Reservas.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FODUN.Reservas.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Alojamiento> Alojamientos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<AlojamientoDisponibleDto> AlojamientoDisponibleDto { get; set; }

        public DbSet<FechasEspeciales> FechasEspeciales { get; set; }
        public object AlojamientoResultadosBusqueda { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(u => u.Id).ValueGeneratedOnAdd();

            // CONFIGURACIÓN DE RELACIONES ENTRE ENTIDADES

            builder.Entity<Alojamiento>()
                .HasOne(a => a.Ubicacion)
                .WithMany(u => u.Alojamientos)
                .HasForeignKey(a => a.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AlojamientoDisponibleDto>().HasNoKey();

            builder.Entity<Tarifa>()
                .HasOne(t => t.Alojamiento)
                .WithMany(a => a.Tarifas)
                .HasForeignKey(t => t.AlojamientoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reserva>()
                .HasOne(r => r.Alojamiento)
                .WithMany(a => a.Reservas)
                .HasForeignKey(r => r.AlojamientoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reserva>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}