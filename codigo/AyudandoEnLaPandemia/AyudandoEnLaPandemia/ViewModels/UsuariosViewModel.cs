﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class UsuariosViewModel
    {

        [Required(ErrorMessage = "Ingresar email")]
        [EmailAddress(ErrorMessage = "Ingrese un mail valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresar contraseña")]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$",
        ErrorMessage = "La contraseña debe tener al menos 1 mayúscula, 1 minúscula, 1 número y tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Reingresar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña deben coincidir")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Ingresar Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresar Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingresar Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        public string UserName { get; set; }


    }
}