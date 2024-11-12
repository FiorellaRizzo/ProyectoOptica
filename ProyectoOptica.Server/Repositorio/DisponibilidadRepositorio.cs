using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using ProyectoOptica.Server.Repositorio;
using Microsoft.EntityFrameworkCore;


namespace ProyectoOptica.Server.Repositorio
{
    public class DisponibilidadRepositorio : Repositorio<Disponibilidad>, IDisponibilidadRepositorio
    {
        private readonly Context context;

        public DisponibilidadRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Disponibilidad?> SelectByFechaHora(int optometristaId, DateTime fecha, TimeSpan hora)
        {
            return await context.Disponibilidades
                .FirstOrDefaultAsync(d => d.OptometristaId == optometristaId &&
                                          d.FechaDisponibilidad.Date == fecha.Date &&
                                          d.FechaDisponibilidad.TimeOfDay == hora && 
                                          d.Estado == true);
        }


    }
}
