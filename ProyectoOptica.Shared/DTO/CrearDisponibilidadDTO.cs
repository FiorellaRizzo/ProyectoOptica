using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CrearDisponibilidadDTO
    {
        [Required(ErrorMessage = "El ID del optometrista es obligatorio")]
        public int OptometristaId { get; set; }

        [Required(ErrorMessage = "Colocar las fechas que tenga disponible")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "Colocar los horarios que tenga disponible")]
        public TimeSpan HoraDisponible { get; set; }

        public bool Estado { get; set; } = true; // Indica si está disponible o no
    }

}
