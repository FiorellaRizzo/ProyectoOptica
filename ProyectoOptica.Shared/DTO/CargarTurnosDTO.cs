using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.Shared.DTO
{
    public class CargarTurnosDTO
    {
        public DateTime Desde { get; set; }   // local
        public DateTime Hasta { get; set; }   // local
        public int HoraInicio { get; set; }   // local 0–23
        public int HoraFin { get; set; }      // local 1–24
        public List<int>? DiasSemana { get; set; } // 0=Dom,1=Lun,...6=Sab (opcional)
    }

}
