using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
    public class Disponibilidad : EntityBase
    {
        public int IdDisponibilidad { get; set; } // Agrega esta propiedad si necesitas identificar la disponibilidad
        public int OptometristaId { get; set; }
        public Optometrista Optometristas { get; set; }

        [Required(ErrorMessage = "Colocar las fechas que tenga disponible")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "Colocar los horarios que tenga disponible")]
        public TimeSpan HoraDisponible { get; set; }

        public bool Estado { get; set; } = true; // Indica si está disponible o no
    }

}