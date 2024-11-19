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

            public int IdDisponibilidad { get; set; } // Este campo será opcional y no es necesario enviarlo al servidor

         
            [Required(ErrorMessage = "El ID del optometrista es obligatorio")]
            public int OptometristaId { get; set; }

            [Required(ErrorMessage = "La fecha de disponibilidad es obligatoria")]
            public DateTime FechaDisponibilidad { get; set; }

            [Required(ErrorMessage = "La hora de disponibilidad es obligatoria")]
            public TimeSpan HoraDisponible { get; set; }
        
          // public bool Estado { get; set; } = true; // Indica si está disponible o no
    }

}
