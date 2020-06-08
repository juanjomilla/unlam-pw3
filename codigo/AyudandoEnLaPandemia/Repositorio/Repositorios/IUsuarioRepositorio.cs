using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public interface IUsuarioRepositorio : IRepository<Usuarios>
    {
        Usuarios BuscarUsuario(Usuarios usuario);

        void CrearUsuario(Usuarios usuarioNuevo);
    }
}
