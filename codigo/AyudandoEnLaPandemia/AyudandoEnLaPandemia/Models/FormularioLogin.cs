using AyudandoEnLaPandemia.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.Models
{
    public class FormularioLogin : BaseViewModel
    {
        [Required(ErrorMessage = "Ingresar email")]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Ingresar contraseña")]
        public string password { get; set; }
    }
}