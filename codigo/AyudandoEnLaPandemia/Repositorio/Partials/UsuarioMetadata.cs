using System.ComponentModel.DataAnnotations;

namespace Repositorio
{
    internal class UsuarioMetadata
    {
        [Required(ErrorMessage = "Ingresar email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresar contraseña")]
        public string Password { get; set; }
     
    }
}