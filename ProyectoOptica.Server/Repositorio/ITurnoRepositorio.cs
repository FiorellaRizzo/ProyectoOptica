using ProyectoOptica.BD.Data.Entity;

namespace ProyectoOptica.Server.Repositorio
{
   
    public interface ITurnoRepositorio : IRepositorio<Turno>
    {
        Task<int> CrearRangoAsync(DateTime desdeUtc, DateTime hastaUtc, int horaInicio, int horaFin, int[]? diasSemana);
        Task<List<Turno>> ObtenerDisponiblesAsync(DateTime? desdeUtc, DateTime? hastaUtc);
    }
}

