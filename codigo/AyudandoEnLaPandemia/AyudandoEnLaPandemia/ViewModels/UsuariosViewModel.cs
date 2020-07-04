using System;
using System.ComponentModel.DataAnnotations;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class UsuariosViewModel
    {

        [Required(ErrorMessage = "Ingresar email")]
        [EmailAddress(ErrorMessage = "Ingrese un mail valido")]
        [StringLength(50, ErrorMessage = "No debe tener más de 50 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresar contraseña")]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$",
        ErrorMessage = "La contraseña debe tener al menos 1 mayúscula, 1 minúscula, 1 número y tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Reingresar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña deben coincidir")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Ingresar Fecha de Nacimiento")]
        [CustomValidation(typeof(UsuariosViewModel), "ValidarMayorEdad")]
        public DateTime FechaNacimiento { get; set; }
       
        public int Edad { get { return DateTime.Now.Year - FechaNacimiento.Year; } }

        public static ValidationResult ValidarMayorEdad(object value, ValidationContext context)
        {
            var usuarioFormulario = context.ObjectInstance as UsuariosViewModel;

            if (usuarioFormulario.Edad >= 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Debe ser mayor de edad para registrarse");
            }

        }

    }
}