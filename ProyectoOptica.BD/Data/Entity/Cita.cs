using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
    [Index(nameof(ClienteId), nameof(DisponibilidadId), Name = "Cita_UQ", IsUnique = true)]

    public class Cita : EntityBase
    {
        [Required(ErrorMessage = "El usuario de la persona es obligatorio")]
        public int ClienteId { get; set; }
        public Cliente Clientes { get; set; }
        public int DisponibilidadId { get; set; }
        public Disponibilidad Disponibilidades { get; set; }

        [Required(ErrorMessage = "Seleccionar una fecha disponible")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "Seleccionar un horario disponible")]
        public TimeSpan HoraDisponible { get; set; }

        public bool Estado { get; set; } = true; // si está reservada la cita o no
    }

}