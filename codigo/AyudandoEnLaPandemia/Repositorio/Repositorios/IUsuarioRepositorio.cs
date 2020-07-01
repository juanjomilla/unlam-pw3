using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuarios>
    {
        Usuarios BuscarUsuario(Usuarios usuario);

        void CrearUsuario(Usuarios usuarioNuevo);
        void ValidarUsuario(int IdUsuario, string token);
        bool ValidarEmail(string email);
        void ActualizarPerfil(string nombre, string apellido, DateTime fechaNacimiento, string foto, int idUsuario);
    }
}
