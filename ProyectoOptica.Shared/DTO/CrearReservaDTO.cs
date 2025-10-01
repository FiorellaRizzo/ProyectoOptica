using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


    namespace ProyectoOptica.Shared.DTO
    {
        public class CrearReservaDTO
        {
            [Required] public int TurnoId { get; set; }

            [Required, MaxLength(150)]
            public string NombreCliente { get; set; } = null!;

            [Required, MaxLength(150), EmailAddress]
            public string EmailCliente { get; set; } = null!;

            [MaxLength(20)] public string? Telefono { get; set; }
            [MaxLength(200)] public string? Notas { get; set; }
        }
    }


