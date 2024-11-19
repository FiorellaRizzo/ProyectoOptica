using ProyectoOptica.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CrearCitaDTO
    {
        
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El ID de disponibilidad es obligatorio")]
        public int DisponibilidadId { get; set; }

        [Required(ErrorMessage = "Seleccionar una fecha disponible")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "Seleccionar un horario disponible")]
        public TimeSpan HoraDisponible { get; set; }
    }

}
