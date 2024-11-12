using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/Cita")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaRepositorio repositorio;
        private readonly IMapper mapper;

        public CitaController(ICitaRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }


        [HttpGet] // api/Cita

        public async Task<ActionResult<List<Cita>>> Get()
        {
            return await repositorio.Select();
        }


        [HttpGet("{id:int}")] //api/Cita/2
        public async Task<ActionResult<Cita>> Get(int id)
        {
            Cita? cita = await repositorio.SelectById(id);
            if (cita == null)
            {
                return NotFound();
            }
            return cita;
        }


        [HttpPost] // api/Cita
        public async Task<ActionResult<int>> Post(CrearCitaDTO crearCitaDto)
        {
            try
            {
                var cita = mapper.Map<Cita>(crearCitaDto);
                var citaId = await repositorio.Insert(cita);
                return Ok(citaId);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("existe/{id:int}")] //api/Cita/existe/2
        public async Task<ActionResult<bool>> Existe(int id)
        {
            return await repositorio.Existe(id);
        }



        // Actualiza una cita existente
        [HttpPut("{id:int}")] // api/Cita/2
        public async Task<ActionResult> ActualizarCita(int id, [FromBody] Cita entidad)
        {
            // Verificar que el ID en la URL coincida con el ID en la entidad
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            // Buscar la cita existente en la base de datos
            var citaExistente = await repositorio.SelectById(id);


            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada.");
            }

            // Actualizar las propiedades de la cita existente con los datos proporcionados
            citaExistente.ClienteId = entidad.ClienteId;
            citaExistente.DisponibilidadId = entidad.DisponibilidadId;
            citaExistente.FechaDisponibilidad = entidad.FechaDisponibilidad;
            citaExistente.HoraDisponible = entidad.HoraDisponible;
            citaExistente.Estado = entidad.Estado;

            try
            {
                await repositorio.update(id, citaExistente);

                return Ok("Cita actualizada exitosamente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        // Elimina una cita específica por ID
        [HttpDelete("{id:int}")] // api/reservas/2
        public async Task<ActionResult> EliminarCita(int id)
        {
            // Verificar si existe una cita con el ID proporcionado
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return NotFound($"La cita con ID {id} no existe.");
            }



            if (await repositorio.Borrar(id))
            {
                return Ok("Cita eliminada exitosamente.");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
