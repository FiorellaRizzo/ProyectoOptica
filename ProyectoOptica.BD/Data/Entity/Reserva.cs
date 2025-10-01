using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
    //  reserva  única por turno 
    [Index(nameof(TurnoId), Name = "UQ_Reserva_Turno", IsUnique = true)]
    public class Reserva : EntityBase
    {
        [Required]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; } = null!;

        // Datos del cliente 
        [Required, MaxLength(150)]
        public string NombreCliente { get; set; } = null!;

        [Required, MaxLength(150), EmailAddress]
        public string EmailCliente { get; set; } = null!;

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(200)]
        public string? Notas { get; set; }

        public DateTime CreadaUtc { get; set; } = DateTime.UtcNow;
    }
}

