using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Shared.DTO;
using System;


namespace ProyectoOptica.Server.Controllers
{
    /// <summary>
    /// Controlador para gestionar todas las operaciones de reserva de citas.
    /// </summary>
    [ApiController]
    [Route("api/reservas")]
    public class ReservarCitaControllers : ControllerBase
    {
        public readonly Context context;

        public   ReservarCitaControllers (Context context)
        {
            this.context = context;
        }

        [HttpGet] //Es como el select en sql server
        public async Task<ActionResult<List<Cita>>> Get() // es de manera asincrona para optimizar el funcionamiento de las capas
        {
            return await context.Citas.ToListAsync();
        }

        // Obtiene todas las citas
        [HttpGet("Citas")]
        public async Task<IActionResult> ObtenerTodasLasCitas()
        {
            var citas = await context.Citas
                .Include(c => c.Clientes)
                .Include(c => c.Disponibilidades)
                .ToListAsync();
            return Ok(citas);
        }

        // Obtiene una cita específica por ID  sin las entidades relacionadas
        [HttpGet("{Id:int}")] 
        public async Task<ActionResult<Cita>> Get(int Id)
        {
            var Cita = await context.Citas
                                    .FirstOrDefaultAsync(x => x.Id == Id);

            if (Cita == null)
            {
                return NotFound("Cita no encontrada.");
            }
            return Ok(Cita);
        }

        // Obtiene la cita junto con la información de Clientes y Disponibilidades
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCitaPorId(int id)
        {
            var cita = await context.Citas
                .Include(c => c.Clientes)
                .Include(c => c.Disponibilidades)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }
            return Ok(cita);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCitaDTO entidadDTO)
        {
            try
            {
                // Crea una nueva instancia de Cita
                Cita entidad = new Cita();

                // Verificar si el cliente existe en la base de datos
                var clienteExistente = await context.Clientes.FindAsync(entidadDTO.Clientes.Id);
                if (clienteExistente == null)
                {
                    return BadRequest("El cliente ingresado no existe en la base de datos.");
                }

                // Verificar si la disponibilidad existe en la base de datos
                var disponibilidadExistente = await context.Disponibilidades.FindAsync(entidadDTO.Disponibilidades.Id);
                if (disponibilidadExistente == null)
                {
                    return BadRequest("La disponibilidad proporcionada no existe .");
                }

                // Asignar los valores del DTO a la nueva entidad Cita
                entidad.Clientes = clienteExistente;
                entidad.Disponibilidades = disponibilidadExistente;
                entidad.FechaDisponibilidad = entidadDTO.FechaDisponibilidad;
                entidad.HoraDisponible = entidadDTO.HoraDisponible;
                entidad.Estado = true; // Estado inicial por defecto

                // Agregar la entidad al contexto y guardar los cambios
                context.Citas.Add(entidad);
                await context.SaveChangesAsync();

                // Devolver el ID de la nueva cita creada
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Actualiza una cita existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCita(int id, [FromBody] Cita citaActualizada)
        {
            var citaExistente = await context.Citas.FindAsync(id);
            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada.");
            }

            // Actualizamos los datos de la cita existente con los datos proporcionados
            citaExistente.ClienteId = citaActualizada.ClienteId;
            citaExistente.DisponibilidadId = citaActualizada.DisponibilidadId;
            citaExistente.FechaDisponibilidad = citaActualizada.FechaDisponibilidad;
            citaExistente.HoraDisponible = citaActualizada.HoraDisponible;
            citaExistente.Estado = citaActualizada.Estado;

            await context.SaveChangesAsync();
            return Ok("Cita actualizada exitosamente.");
        }

        // Elimina una cita específica por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCita(int id)
        {
            var cita = await context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            context.Citas.Remove(cita);
            await context.SaveChangesAsync();
            return Ok("Cita eliminada exitosamente.");
        }

        // Obtiene la disponibilidad de días y horarios (simulación)
        [HttpGet("disponibilidad")]
        public IActionResult ObtenerDisponibilidad()
        {
            var disponibilidad = new List<string> { "Lunes 10:00", "Martes 14:00", "Miércoles 16:00" };
            return Ok(disponibilidad);
        }

        // Verifica si un horario específico está disponible
        [HttpPost("disponibilidad/verificar")]
        public IActionResult VerificarDisponibilidad([FromBody] Cita solicitud)
        {
            bool disponible = !context.Citas.Any(c => c.FechaDisponibilidad.Date == solicitud.FechaDisponibilidad.Date && c.HoraDisponible.TimeOfDay == solicitud.HoraDisponible.TimeOfDay);
            return disponible ? Ok("La fecha y hora solicitadas están disponibles.") : Conflict("Fecha y hora no disponibles.");
        }
    }

}
