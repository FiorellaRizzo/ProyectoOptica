using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;

namespace ProyectoOptica.Server.Repositorio
{
    public interface ICitaRepositorio : IRepositorio<Cita>
    {
        Task<Cita?> SelectByFechaHora(int clienteId, DateTime fecha, TimeSpan hora);
        // Otros métodos del repositorio de Cita
    }

   
}
