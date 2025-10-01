//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;
using System.Linq;


namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/Turnos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoRepositorio repositorio;
        private readonly IOutputCacheStore outputCacheStore;   

        // Tag para invalidar todas las variantes cacheadas del endpoint disponibles
        private const string cacheKey = "TurnosDisponibles";

        public TurnosController(ITurnoRepositorio repositorio,
                                IOutputCacheStore outputCacheStore)  
        {
            this.repositorio = repositorio;
            this.outputCacheStore = outputCacheStore;            
        }

        // GET api/Turnos
        [HttpGet]
        [OutputCache(Tags = [cacheKey])]
        [AllowAnonymous]
        public async Task<ActionResult<List<Turno>>> Get()
        {
            return await repositorio.Select();
        }

        // GET api/Turnos/5
        [HttpGet("{id:int}")]
        [OutputCache(Tags = [cacheKey])]
        public async Task<ActionResult<Turno>> Get(int id)
        {
            var turno = await repositorio.SelectById(id);
            if (turno == null) return NotFound();
            return turno;
        }

        // GET api/Turnos/existe/5
        [HttpGet("existe/{id:int}")]
        [OutputCache(Tags = [cacheKey])]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            return await repositorio.Existe(id);
        }



        [HttpGet("disponibles")]
        [OutputCache(  Tags = new[] { "TurnosDisponibles" },
                       VaryByQueryKeys = new[] { "desde", "hasta" })]
        public async Task<ActionResult<List<TurnoDisponibleDTO>>> Disponibles(
        [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)   
        {
            var list = await repositorio.ObtenerDisponiblesAsync(desde, hasta);

            var dto = list.Select(t => new TurnoDisponibleDTO
            {
                Id = t.Id,
                FechaHora = t.FechaHora     
            }).ToList();

            return Ok(dto);
        }

        [HttpPost("cargar")]
        //[AllowAnonymous]
        public async Task<ActionResult<int>> Cargar([FromBody] CargarTurnosDTO dto)
        {
            
            if (dto.HoraInicio < 0 || dto.HoraInicio > 23 ||
                dto.HoraFin < 1 || dto.HoraFin > 24 || dto.HoraFin <= dto.HoraInicio)
                return BadRequest("Rango horario inválido.");

            
            var desde = dto.Desde.Date;
            var hasta = dto.Hasta.Date;
            if (desde > hasta)
                return BadRequest("Desde debe ser <= Hasta.");

            // dto.DiasSemana viene como List<int>? → convertimos a arreglo y manejamos null
            var dias = dto.DiasSemana?.ToArray() ?? Array.Empty<int>();

            // Creamos los turnos
            var creados = await repositorio.CrearRangoAsync(desde, hasta, dto.HoraInicio, dto.HoraFin, dias);

            // Invalidamos caché de “disponibles”
            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(creados);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Turno entidad)
        {
            // Mismo patrón que usa tu profe: id de ruta debe coincidir con el body
            if (id != entidad.Id)
                return BadRequest("Datos incorrectos.");

            var actual = await repositorio.SelectById(id);
            if (actual is null)
                return NotFound("El turno no existe.");

            // normalizar a minutos exactos si te interesa
            entidad.FechaHora = new DateTime(
                entidad.FechaHora.Year, entidad.FechaHora.Month, entidad.FechaHora.Day,
                entidad.FechaHora.Hour, entidad.FechaHora.Minute, 0);

            // no se permite cambiar acá el estado de reserva.
            // Eso lo hace el flujo de Reservas. Solo dejamos mover el horario 
            actual.FechaHora = entidad.FechaHora;
            actual.DuracionMinutos = entidad.DuracionMinutos;

            var ok = await repositorio.Update(id, actual);
            if (!ok)
                return BadRequest("No se pudo actualizar el turno.");

            // Limpiamos el caché de “disponibles” para que se regenere con el nuevo horario
            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok();
        }

        // DELETE api/Turnos/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await repositorio.Borrar(id);

            //si se borra un turno cambia la disponibilidad
            if (ok) await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return ok ? Ok() : NotFound();
        }
    }
}

