using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CrearCita_DisponibilidadDTO
    {
        // Datos de la Cita

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "La hora de la cita es obligatoria.")]
        public TimeSpan HoraCita { get; set; }

        public bool EstadoCita { get; set; } = true; // Indica si la cita está activa o no

        // Datos de la Disponibilidad

        [Required(ErrorMessage = "El ID del optometrista es obligatorio.")]
        public int OptometristaId { get; set; }

        [Required(ErrorMessage = "La fecha de disponibilidad es obligatoria.")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "La hora de disponibilidad es obligatoria.")]
        public TimeSpan HoraDisponible { get; set; }

        public bool EstadoDisponibilidad { get; set; } = true; // Indica si la disponibilidad está activa o no
    }

}
