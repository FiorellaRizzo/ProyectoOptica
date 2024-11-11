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
    [Route("api/reservas")]
    public class ReservarCitaControllers : ControllerBase
    {
        private readonly IReservarCitaRepositorio repositorio;
        private readonly IMapper mapper;

        public ReservarCitaControllers( IReservarCitaRepositorio repositorio,
                                        IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // Método para obtener todas las citas
        [HttpGet]
        public async Task<ActionResult<List<Cita>>> Get() // es de manera asincrona para optimizar el funcionamiento de las capas
        {
            return await repositorio.Select();
                
        }


        

        [HttpGet("{id:int}")] //api/Reserva/2
        public async Task<ActionResult<Cita>> Get(int id)
        {

            Cita? cita = await repositorio.SelectById(id);
           if (cita == null)
           {
                return NotFound("Cita no encontrada.");
           }
            return cita;
        }         
                
        // Método para crear una nueva cita
       [HttpPost]
       public async Task<ActionResult<int>> Post(CrearCitaDTO entidadDTO)
       {
            try
            {
                // Verificar si el cliente existe
                var clienteExistente = await repositorio.ObtenerClientePorIdAsync(entidadDTO.Clientes.Id);
                if (clienteExistente == null)
                {
                    return BadRequest("El cliente ingresado no existe en la base de datos.");
                }

                // Verificar si la disponibilidad existe
                var disponibilidadExistente = await repositorio.ObtenerDisponibilidadPorIdAsync(entidadDTO.Disponibilidades.Id);
                if (disponibilidadExistente == null)
                {
                    return BadRequest("La disponibilidad proporcionada no existe.");
                }

                // Mapear el DTO a la entidad Cita
                var entidad = mapper.Map<Cita>(entidadDTO);
                entidad.Clientes = clienteExistente;
                entidad.Disponibilidades = disponibilidadExistente;


                return await repositorio.Insert(entidad);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }



        // Actualiza una cita existente
        [HttpPut("{id:int}")] // api/reservas/2
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



       
    