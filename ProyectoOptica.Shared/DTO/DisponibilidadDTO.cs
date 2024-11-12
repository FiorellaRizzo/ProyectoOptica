using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class DisponibilidadDTO
    {
        public int IdDisponibilidad { get; set; }
        public int IdOptometrista { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
        public bool Disponible { get; set; } // Indica si el horario está disponible o no
    }
}
