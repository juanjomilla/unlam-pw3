﻿using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioRegistrar
    {
        public static void crearRegistro(Usuarios usuarioNuevo)
        {
            UsuarioRepositorio.crearUsuario(usuarioNuevo);
        }
    }
}