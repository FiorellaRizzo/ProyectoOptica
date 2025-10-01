using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
 
    [Index(nameof(EstaReservado), nameof(FechaHora), Name = "Turno_Disponibles_IX", IsUnique = false)]
    public class Turno : IEntityBase
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }          // ← HORA LOCAL (AR)
        public bool EstaReservado { get; set; }
        public int DuracionMinutos { get; set; } = 45;
       public DateTime Creada { get; set; } = DateTime.Now; // local

        
         public Reserva? Reserva { get; set; }
    }
}
