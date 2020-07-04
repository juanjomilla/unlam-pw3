using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels.MiPerfil
{
    public class MiPerfilViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingresar Nombre")]
        [StringLength(50, ErrorMessage = "No debe tener más de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresar Apellido")]
        [StringLength(50, ErrorMessage = "No debe tener más de 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingresar Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
    }
}