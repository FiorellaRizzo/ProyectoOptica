using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;

namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/Disponibilidad")]
    public class DisponibilidadController : ControllerBase
    {
        private readonly IDisponibilidadRepositorio repositorio;
        private readonly IMapper mapper;

        public DisponibilidadController(IDisponibilidadRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // Obtener todas las disponibilidades
        [HttpGet] // api/Disponibilidad
        public async Task<ActionResult<List<Disponibilidad>>> Get()
        {
            return await repositorio.Select();
        }

        // Obtener una disponibilidad por ID
        [HttpGet("{id:int}")] // api/Disponibilidad/2
        public async Task<ActionResult<Disponibilidad>> Get(int id)
        {
            Disponibilidad? disponibilidad = await repositorio.SelectById(id);
            if (disponibilidad == null)
            {
                return NotFound();
            }
            return disponibilidad;
        }

        // Crear una nueva disponibilidad
        [HttpPost] // api/Disponibilidad
        public async Task<ActionResult<int>> Post(CrearDisponibilidadDTO crearDisponibilidadDto)
        {
            try
            {
                var disponibilidad = mapper.Map<Disponibilidad>(crearDisponibilidadDto);
                var disponibilidadId = await repositorio.Insert(disponibilidad);
                return Ok(disponibilidadId);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        // Verificar si una disponibilidad existe por ID
        [HttpGet("existe/{id:int}")] // api/Disponibilidad/existe/2
        public async Task<ActionResult<bool>> Existe(int id)
        {
            return await repositorio.Existe(id);
        }

        // Actualizar una disponibilidad existente
        [HttpPut("{id:int}")] // api/Disponibilidad/2
        public async Task<ActionResult> ActualizarDisponibilidad(int id, [FromBody] Disponibilidad entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var disponibilidadExistente = await repositorio.SelectById(id);

            if (disponibilidadExistente == null)
            {
                return NotFound("Disponibilidad no encontrada.");
            }

            disponibilidadExistente.OptometristaId = entidad.OptometristaId;
            disponibilidadExistente.FechaDisponibilidad = entidad.FechaDisponibilidad;
            disponibilidadExistente.HoraDisponible = entidad.HoraDisponible;
            disponibilidadExistente.Estado = entidad.Estado;

            try
            {
                await repositorio.update(id, disponibilidadExistente);
                return Ok("Disponibilidad actualizada exitosamente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Eliminar una disponibilidad específica por ID
        [HttpDelete("{id:int}")] // api/Disponibilidad/2
        public async Task<ActionResult> EliminarDisponibilidad(int id)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return NotFound($"La disponibilidad con ID {id} no existe.");
            }

            if (await repositorio.Borrar(id))
            {
                return Ok("Disponibilidad eliminada exitosamente.");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
