using ProyectoOptica.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CrearCitaDTO
    {
        [Required(ErrorMessage = "El usuario de la persona es obligatorio")]
        public int ClienteId { get; set; }
        public Cliente Clientes { get; set; }
        public int DisponibilidadId { get; set; }
        public Disponibilidad Disponibilidades { get; set; }

        [Required(ErrorMessage = "Seleccionar una fecha disponible")]
        public DateTime FechaDisponibilidad { get; set; }

        [Required(ErrorMessage = "Seleccionar un horario disponible")]
        public DateTime HoraDisponible { get; set; }

    }
}
