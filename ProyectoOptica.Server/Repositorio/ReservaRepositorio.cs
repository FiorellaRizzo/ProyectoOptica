using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;

namespace ProyectoOptica.Server.Repositorio
{
    public class ReservaRepositorio : Repositorio<Reserva>, IReservaRepositorio
    {
        private readonly Context _ctx;
        public ReservaRepositorio(Context ctx) : base(ctx) { _ctx = ctx; }

        public async Task<int> ReservarAsync(Reserva reserva)
        {
            // 1) validar turno
            var turno = await _ctx.Turnos.FirstOrDefaultAsync(t => t.Id == reserva.TurnoId);
            if (turno is null) throw new InvalidOperationException("El turno no existe.");
            if (turno.EstaReservado) throw new InvalidOperationException("El turno ya está reservado.");

            // 2) marcar reservado + crear reserva en una sola operación
            turno.EstaReservado = true;
            await _ctx.Reservas.AddAsync(reserva);

            try
            {
                await _ctx.SaveChangesAsync();
                return reserva.Id;
            }
            catch (DbUpdateException)
            {
                // Por si corre en carrera y salta el índice único de TurnoId
                throw new InvalidOperationException("No se pudo reservar: el turno fue tomado por otro usuario.");
            }
        }

        public async Task<bool> CancelarAsync(int reservaId)
        {
            var reserva = await _ctx.Reservas
                                    .Include(r => r.Turno)
                                    .FirstOrDefaultAsync(r => r.Id == reservaId);
            if (reserva is null) return false;

            if (reserva.Turno is not null)
                reserva.Turno.EstaReservado = false;

            _ctx.Reservas.Remove(reserva);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<Reserva?> ObtenerPorTurnoAsync(int turnoId)
        {
            return await _ctx.Reservas
                .Include(r => r.Turno)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.TurnoId == turnoId);
        }

        public async Task<List<Reserva>> SelectConTurnoAsync()
        {
            return await _ctx.Reservas
                .Include(r => r.Turno)
                .AsNoTracking()
                .OrderBy(r => r.Turno.FechaHora) 
                .ToListAsync();
        }
    }
}
