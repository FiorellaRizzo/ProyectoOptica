using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
   
        public class ReservaDTO
        {
            public int Id { get; set; }
            public int TurnoId { get; set; }
            public DateTime FechaHoraUtc { get; set; }

            public string NombreCliente { get; set; } = null!;
            public string EmailCliente { get; set; } = null!;
            public string? Telefono { get; set; }
            public string? Notas { get; set; }

            public DateTime CreadaUtc { get; set; }
        }
    }

