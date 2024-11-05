using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/reservas")]
    public class ReservarCitaControllers : ControllerBase
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public ReservarCitaControllers(Context context, 
                                       IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /*/* Método para obtener todas las citas
        [HttpGet("todas")]
        public async Task<ActionResult<List<Cita>>> Get() // es de manera asincrona para optimizar el funcionamiento de las capas
        {
            return await context.Citas.ToListAsync();
        }/*/


        // Método para listar citas usando ListaDeCitasDTO
        [HttpGet("listadocitas")]
        public async Task<ActionResult<IEnumerable<ListaDeCitasDTO>>> ListarCitas()
        {
            try
            {
                var citas = await context.Citas
                    .Include(c => c.Disponibilidades)
                    .ThenInclude(d => d.Optometristas)
                    .ToListAsync();

                var listaCitasDTO = mapper.Map<List<ListaDeCitasDTO>>(citas);
                return Ok(listaCitasDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Método para obtener el detalle de una cita específica
        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<DetalleCitaDTO>> ObtenerDetalleCita(int id)
        {
            try
            {
                var cita = await context.Citas
                    .Include(c => c.Clientes)
                    .Include(c => c.Disponibilidades)
                    .ThenInclude(d => d.Optometristas)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cita == null)
                {
                    return NotFound("La cita no existe.");
                }

                var detalleCitaDTO = mapper.Map<DetalleCitaDTO>(cita);
                return Ok(detalleCitaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener una cita específica por ID sin las entidades relacionadas
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Cita>> Get(int Id)
        {
            var cita = await context.Citas.FirstOrDefaultAsync(x => x.Id == Id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }
            return Ok(cita);
        }

        // Resto del código sin cambios

        // Método para crear una nueva cita
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCitaDTO entidadDTO)
        {
            try
            {
                var clienteExistente = await context.Clientes.FindAsync(entidadDTO.Clientes.Id);
                if (clienteExistente == null)
                {
                    return BadRequest("El cliente ingresado no existe en la base de datos.");
                }

                var disponibilidadExistente = await context.Disponibilidades.FindAsync(entidadDTO.Disponibilidades.Id);
                if (disponibilidadExistente == null)
                {
                    return BadRequest("La disponibilidad proporcionada no existe.");
                }

                Cita nuevaEntidad = mapper.Map<Cita>(entidadDTO);

                context.Citas.Add(nuevaEntidad);
                await context.SaveChangesAsync();

                return nuevaEntidad.Id;
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
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
