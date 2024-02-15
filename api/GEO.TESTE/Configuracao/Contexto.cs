using api.Configuracao.Map;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Configuracao
{
    public class Contexto: DbContext
    {
            public Contexto(DbContextOptions<Contexto> options) : base(options)
            {
                Database.EnsureCreated();
            }

            public DbSet<Geofencing> Geofencing { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new GeofencingMap());

                base.OnModelCreating(modelBuilder);
            }
    }
}
