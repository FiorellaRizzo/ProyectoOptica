using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class ListaDeCitasDTO
    {
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }

        // Info del Optometrista
        public string OptometristaNombre { get; set; }
    }
}
