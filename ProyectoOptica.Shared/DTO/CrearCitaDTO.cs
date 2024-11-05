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

       
        
        public Cliente Clientes { get; set; }
       
        public Disponibilidad Disponibilidades { get; set; }

       
        public DateTime FechaDisponibilidad { get; set; }

        public DateTime HoraDisponible { get; set; }
    }
}
