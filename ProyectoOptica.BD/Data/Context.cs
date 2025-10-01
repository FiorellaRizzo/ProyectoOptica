using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace ProyectoOptica.BD.Data
{
    public class Context : IdentityDbContext
    {
       
        public DbSet<Turno> Turnos { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

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



            // Relación 1–1: Reserva (FK) -> Turno
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Turno)
                .WithOne(t => t.Reserva)
                .HasForeignKey<Reserva>(r => r.TurnoId)
                .HasConstraintName("FK_Reservas_Turnos_TurnoId");



            base.OnModelCreating(modelBuilder);
        }
    }

}


