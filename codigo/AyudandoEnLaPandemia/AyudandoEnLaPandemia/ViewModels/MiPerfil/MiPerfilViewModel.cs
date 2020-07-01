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
        //[CustomValidation(typeof(UsuariosViewModel), "ValidarMayorEdad")]
        public DateTime FechaNacimiento { get; set; }

        public int edad { get { return DateTime.Now.Year - FechaNacimiento.Year; } }

        [StringLength(20, ErrorMessage = "No debe tener más de 20 caracteres")]
        public string UserName { get; set; }
        public string Foto { get; set; }

        //public static ValidationResult ValidarMayorEdad(object value, ValidationContext context)
        //{
        //    var usuarioFormulario = context.ObjectInstance as UsuariosViewModel;

        //    if (usuarioFormulario.edad >= 18)
        //    {
        //        return ValidationResult.Success;
        //    }
        //    else
        //    {
        //        return new ValidationResult("Debe ser mayor de edad para registrarse");
        //    }

        //}
    }
}