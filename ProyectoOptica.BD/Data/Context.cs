using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<TDocumento> TDocumentos { get; set; }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Optometrista> Optometristas { get; set; }

        public DbSet<Cita> Citas { get; set; }

        public DbSet<Disponibilidad> Disponibilidades { get; set; }



        public Context(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                               .SelectMany(t => t.GetForeignKeys())
                                               .Where(fk => !fk.IsOwnership
                                                            && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

  