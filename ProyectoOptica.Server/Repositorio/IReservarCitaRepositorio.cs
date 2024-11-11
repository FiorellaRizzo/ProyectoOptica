using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public interface IReservarCitaRepositorio : IRepositorio<Cita> 
    {

        Task<Cliente> ObtenerClientePorIdAsync(int id);
         Task<Disponibilidad> ObtenerDisponibilidadPorIdAsync(int id);
         Task AgregarCitaAsync(Cita cita);
         Task<Cita> ObtenerCitaPorIdAsync(int id);
         Task<List<Cita>> ObtenerTodasLasCitasAsync();

    }
}
