using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace ProyectoOptica.Server.Repositorio
{
    public class CitaRepositorio : Repositorio<Cita>, ICitaRepositorio
     
    {
        private readonly Context context;

        public CitaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Cita?> SelectByFechaHora(int clienteId, DateTime fecha, TimeSpan hora)
        {
            return await context.Citas
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId &&
                                          c.FechaDisponibilidad.Date == fecha.Date &&
                                          c.HoraDisponible == hora);
        }
    }
}
