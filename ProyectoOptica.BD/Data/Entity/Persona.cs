using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOptica.BD.Data.Entity
{
   

    [Index(nameof(TDocumentoId), nameof(NumDoc), Name = "Persona_UQ", IsUnique = true)]

    [Index(nameof(Apellido), nameof(Nombre), Name = "Persona_Apellido_Nombre", IsUnique = false)]


    public class Persona : EntityBase
    {
        [Required(ErrorMessage = "El Numero de Documento es obligatorio ")]
        [MaxLength(12, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string NumDoc { get; set; }

        [Required(ErrorMessage = "El Nombre de la persona es obligatorio ")]
        [MaxLength(100, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido de la persona es obligatorio ")]
        [MaxLength(100, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Telefono de la persona es obligatorio ")]
        [MaxLength(15, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Correo de la persona es obligatorio ")]
        [MaxLength(45, ErrorMessage = "Maximo numero de caracteres {1}.")]
        [EmailAddress()]

        public string Correo { get; set; }

        [Required(ErrorMessage = "El Tipo de Documento es obligatorio ")]
        public int TDocumentoId { get; set; }
        public TDocumento TDocumento { get; set; }
    }
}
