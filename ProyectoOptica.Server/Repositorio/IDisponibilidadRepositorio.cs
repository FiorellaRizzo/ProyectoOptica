using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;



namespace ProyectoOptica.Server.Repositorio
{
    public interface IDisponibilidadRepositorio  : IRepositorio<Disponibilidad>
    {
        Task<Disponibilidad> SelectByFechaHora(int optometristaId, DateTime fechaDisponibilidad, TimeSpan horaDisponible);

        public interface IDisponibilidadRepositorio
        {
            Task<Disponibilidad?> SelectByFechaHora(int optometristaId, DateTime fecha, TimeSpan hora);
            // Otros métodos del repositorio de Disponibilidad
        }
    }
}
