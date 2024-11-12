using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoOptica.BD;
using Microsoft.AspNetCore.Mvc;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;

namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/reservas")]
    public class ReservarCitaControllers : ControllerBase
    {
        private readonly ICitaRepositorio repositorioCita;
        private readonly IClienteRepositorio repositorioCliente;
        private readonly IDisponibilidadRepositorio repositorioDisponibilidad;

        public ReservarCitaControllers(ICitaRepositorio repositorioCita,
                                   IClienteRepositorio repositorioCliente,
                                   IDisponibilidadRepositorio repositorioDisponibilidad)
        {
            this.repositorioCita = repositorioCita;
            this.repositorioCliente = repositorioCliente;
            this.repositorioDisponibilidad = repositorioDisponibilidad;
        }

        [HttpGet("GetCitas")]
        public async Task<ActionResult<List<Cita>>> GetCita()
        {
            return await repositorioCita.Select();
        }


        [HttpGet("GetCliente")]
        public async Task<ActionResult<List<Cliente>>> GetCliente()
        {
            return await repositorioCliente.Select();
        }

        [HttpGet("GetDisponibilidad")]
        public async Task<ActionResult<List<Disponibilidad>>> GetDisponibilidad()
        {
            return await repositorioDisponibilidad.Select();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCita_DisponibilidadDTO entidadDTO)
        {
            try
            {
                // Verificar si el optometrista está disponible en la fecha y hora seleccionadas
                var disponibilidadExistente = await repositorioDisponibilidad.SelectByFechaHora(entidadDTO.OptometristaId, entidadDTO.FechaDisponibilidad, entidadDTO.HoraDisponible);
                if (disponibilidadExistente == null || !disponibilidadExistente.Estado)
                {
                    return BadRequest("El optometrista no está disponible en la fecha y hora seleccionadas.");
                }

                // Verificar si ya existe una cita en la misma fecha y hora para el cliente
                var citaExistente = await repositorioCita.SelectByFechaHora(entidadDTO.ClienteId, entidadDTO.FechaCita, entidadDTO.HoraCita);
                if (citaExistente != null)
                {
                    return BadRequest($"El cliente ya tiene una cita programada en la fecha y hora seleccionadas.");
                }

                // Crear la entidad Cita a partir del DTO
                Cita nuevaCita = new Cita
                {
                    ClienteId = entidadDTO.ClienteId,
                    DisponibilidadId = disponibilidadExistente.Id,
                    FechaDisponibilidad = entidadDTO.FechaCita,
                    HoraDisponible = entidadDTO.HoraCita,
                    Estado = entidadDTO.EstadoCita
                };

                int citaId = await repositorioCita.Insert(nuevaCita);
                if (citaId == 0)
                {
                    return BadRequest("No se pudo crear la cita.");
                }

                // Actualizar la disponibilidad para marcarla como reservada
                disponibilidadExistente.Estado = false;
                await repositorioDisponibilidad.update(disponibilidadExistente.Id, disponibilidadExistente);

                return Ok(citaId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
    

























    