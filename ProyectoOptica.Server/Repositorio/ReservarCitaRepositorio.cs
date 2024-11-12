using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace ProyectoOptica.Server.Repositorio
{
    public class ReservarCitaRepositorio : Repositorio<Cita>, IReservarCitaRepositorio
    {
        private readonly Context context;

        public ReservarCitaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Implementación del método para insertar una nueva cita
        public async Task<int> Insert(Cita cita)
        {
            context.Citas.Add(cita);
            await context.SaveChangesAsync();
            return cita.Id; // Asumiendo que IdCita es la clave primaria
        }

        // Implementación del método para seleccionar una cita por ID
        public async Task<Cita> SelectById(int id)
        {
            return await context.Citas.FindAsync(id);
        }

        // Implementación del método para seleccionar todas las citas
        public async Task<List<Cita>> SelectAll()
        {
            return await context.Citas.ToListAsync();
        }

        // Implementación del método para verificar si ya existe una cita para un cliente en una fecha y hora específica
        public async Task<bool> ExisteCitaParaCliente(int clienteId, DateTime fecha, TimeSpan hora)
        {
            return await context.Citas.AnyAsync(c =>
                c.ClienteId == clienteId &&
                c.FechaDisponibilidad == fecha &&
                c.HoraDisponible == hora);
        }

        // Implementación del método para actualizar una cita existente
        public async Task<bool> Update(int id, Cita cita)
        {
            var citaExistente = await context.Citas.FindAsync(id);
            if (citaExistente == null)
            {
                return false;
            }
            context.Entry(citaExistente).CurrentValues.SetValues(cita);
            await context.SaveChangesAsync();
            return true;
        }

        // Implementación del método para eliminar una cita
        public async Task<bool> Delete(int id)
        {
            var cita = await context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }
            context.Citas.Remove(cita);
            await context.SaveChangesAsync();
            return true;
        }

        // Implementación del método para obtener la disponibilidad por ID
        public async Task<Disponibilidad> ObtenerDisponibilidadPorId(int disponibilidadId)
        {
            return await context.Disponibilidades.FindAsync(disponibilidadId);
        }

     
    }

}