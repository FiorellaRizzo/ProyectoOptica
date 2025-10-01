using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;

namespace ProyectoOptica.Server.Repositorio
{
    public class TurnoRepositorio : Repositorio<Turno>, ITurnoRepositorio
    {
        private readonly Context _ctx;
        public TurnoRepositorio(Context ctx) : base(ctx) { _ctx = ctx; }


        public async Task<int> CrearRangoAsync(DateTime desde, DateTime hasta, int horaInicio, int horaFin, int[] diasSemana)
        {
            var filtraDias = diasSemana.Length > 0;

            for (var d = desde.Date; d <= hasta.Date; d = d.AddDays(1))
            {
                if (filtraDias && !diasSemana.Contains((int)d.DayOfWeek)) continue;

                var ini = new DateTime(d.Year, d.Month, d.Day, horaInicio, 0, 0);
                var fin = new DateTime(d.Year, d.Month, d.Day, horaFin, 0, 0);

                for (var t = ini; t < fin; t = t.AddMinutes(45))
                {
                    bool existe = await _ctx.Turnos.AnyAsync(x => x.FechaHora == t);
                    if (!existe) _ctx.Turnos.Add(new Turno { FechaHora = t, EstaReservado = false });
                }
            }

            return await _ctx.SaveChangesAsync();
        }

        public async Task<List<Turno>> ObtenerDisponiblesAsync(DateTime? desde, DateTime? hasta)
        {
            var q = _ctx.Turnos.AsNoTracking().Where(t => !t.EstaReservado);
            if (desde.HasValue) q = q.Where(t => t.FechaHora >= desde.Value);
            if (hasta.HasValue) q = q.Where(t => t.FechaHora <= hasta.Value);
            return await q.OrderBy(t => t.FechaHora).ToListAsync();
        }
    }
}

