using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
    public class Optometrista : EntityBase
    {
        [Required(ErrorMessage = "El usuario de la persona es obligatorio")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public List<Cita> Citas { get; set; }
    }
}
