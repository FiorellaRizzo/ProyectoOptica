using Microsoft.AspNetCore.Mvc;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;

namespace ProyectoOptica.Server.Controllers
{
    [ApiController]
    [Route("api/Reservas")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepositorio _repoReserva;
        private readonly ITurnoRepositorio _repoTurno;

        public ReservasController(IReservaRepositorio repoReserva, ITurnoRepositorio repoTurno)
        {
            _repoReserva = repoReserva;
            _repoTurno = repoTurno;
        }

        // POST api/Reservas
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearReservaDTO dto)
        {
            try
            {
                // Validación básica
                var turno = await _repoTurno.SelectById(dto.TurnoId);
                if (turno is null) return NotFound("El turno no existe.");
                if (turno.EstaReservado) return BadRequest("El turno ya está reservado.");

                var reserva = new Reserva
                {
                    TurnoId = dto.TurnoId,
                    NombreCliente = dto.NombreCliente,
                    EmailCliente = dto.EmailCliente,
                    Telefono = dto.Telefono,
                    Notas = dto.Notas
                };

                var id = await _repoReserva.ReservarAsync(reserva);
                return Ok(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        [HttpGet]                 // GET /api/Reservas
        public async Task<ActionResult<List<ReservaDTO>>> Get()
        {
            var list = await _repoReserva.SelectConTurnoAsync();

            var dto = list.Select(r => new ReservaDTO
            {
                Id = r.Id,
                TurnoId = r.TurnoId,
                FechaHoraUtc = r.Turno?.FechaHora?? default,
                NombreCliente = r.NombreCliente,
                EmailCliente = r.EmailCliente,
                Telefono = r.Telefono,
                Notas = r.Notas,
                CreadaUtc = r.CreadaUtc
            }).ToList();

            return Ok(dto);
        }

        // GET api/Reservas/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservaDTO>> Get(int id)
        {
            var r = await _repoReserva.SelectById(id);
            if (r is null) return NotFound();

            // Map simple a DTO
            return Ok(new ReservaDTO
            {
                Id = r.Id,
                TurnoId = r.TurnoId,
                FechaHoraUtc = r.Turno?.FechaHora ?? default,
                NombreCliente = r.NombreCliente,
                EmailCliente = r.EmailCliente,
                Telefono = r.Telefono,
                Notas = r.Notas,
                CreadaUtc = r.CreadaUtc
            });
        }

        // GET api/Reservas/por-turno/10
        [HttpGet("por-turno/{turnoId:int}")]
        public async Task<ActionResult<ReservaDTO>> GetPorTurno(int turnoId)
        {
            var r = await _repoReserva.ObtenerPorTurnoAsync(turnoId);
            if (r is null) return NotFound();

            return Ok(new ReservaDTO
            {
                Id = r.Id,
                TurnoId = r.TurnoId,
                FechaHoraUtc = r.Turno?.FechaHora?? default,
                NombreCliente = r.NombreCliente,
                EmailCliente = r.EmailCliente,
                Telefono = r.Telefono,
                Notas = r.Notas,
                CreadaUtc = r.CreadaUtc
            });
        }

        // DELETE api/Reservas/5  (cancelar)
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _repoReserva.CancelarAsync(id);
            return ok ? Ok() : NotFound();
        }
    }

}
