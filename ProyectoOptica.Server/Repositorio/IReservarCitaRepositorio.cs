using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public interface IReservarCitaRepositorio : IRepositorio<Cita>
    {// Método para insertar una nueva cita y devolver el ID de la cita creada
        Task<int> Insert(Cita cita);

        // Método para seleccionar una cita por ID
        Task<Cita> SelectById(int id);

        // Método para seleccionar todas las citas 
        Task<List<Cita>> SelectAll();

        // Método para verificar si una cita ya existe para un cliente en una fecha y hora específica
        Task<bool> ExisteCitaParaCliente(int clienteId, DateTime fecha, TimeSpan hora);

        // Método para actualizar una cita existente
        Task<bool> Update(int id, Cita cita);

        // Método para eliminar una cita
        Task<bool> Delete(int id);

        // Método para verificar la disponibilidad por ID de disponibilidad
        Task<Disponibilidad> ObtenerDisponibilidadPorId(int disponibilidadId);

        
    }



}

