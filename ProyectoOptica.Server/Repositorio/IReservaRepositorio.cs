using ProyectoOptica.BD.Data.Entity;

namespace ProyectoOptica.Server.Repositorio
{
    public interface IReservaRepositorio : IRepositorio<Reserva>
    {
        Task<int> ReservarAsync(Reserva reserva);
        Task<bool> CancelarAsync(int reservaId);
        Task<Reserva?> ObtenerPorTurnoAsync(int turnoId);

        // lista completa, incluyendo Turno
        Task<List<Reserva>> SelectConTurnoAsync();
    }
}
