using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class DetalleCitaDTO
    {
        public int IdCita { get; set; }
        public int IdClientes { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }

        // Detalles de Disponibilidad
        public int IdDisponibilidad { get; set; }
        public bool Disponible { get; set; }

        // Detalles del Optometrista
        public int IdOptometrista { get; set; }
        public string OptometristaNombre { get; set; }  
    }
}
