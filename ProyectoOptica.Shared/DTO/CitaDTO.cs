using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CitaDTO
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int ClienteId { get; set; }

        // Información básica del cliente que se quiera exponer (ej. nombre)
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "El ID de disponibilidad es obligatorio")]
        public int DisponibilidadId { get; set; }

        // Información básica de la disponibilidad que se quiera exponer (ej. horario)
        public DateTime FechaDisponibilidad { get; set; }

        public TimeSpan HoraDisponible { get; set; }

        public bool Estado { get; set; } = true;
    }
}
