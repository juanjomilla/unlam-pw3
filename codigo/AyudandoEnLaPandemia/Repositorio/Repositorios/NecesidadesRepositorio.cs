﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Repositorio.Repositorios
{
    public class NecesidadesRepositorio : Repositorio<Necesidades>, INecesidadesRepositorio
    {
        public NecesidadesRepositorio(Contexto dbContext) : base(dbContext) { }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top)
        {
            IQueryable<Necesidades> query = _dbSet;

            return query
                .Where(x => x.Estado != 3 && x.Estado != 4)
                .OrderByDescending(x => x.Valoracion)
                .Take(top);
        }

        public IEnumerable<Necesidades> BuscarNecesidades(string busqueda, int idUsuario)
        {
            IQueryable<Necesidades> query = _dbSet;

            var necesidades = query
                .Where(x => x.Usuarios.Nombre.Contains(busqueda) || x.Usuarios.Apellido.Contains(busqueda) || x.Nombre.Contains(busqueda))
                .Where(x => x.Estado != 3 && x.Estado != 4)
                .Where(x => x.IdUsuarioCreador != idUsuario)
                .ToList();

            // los agrupo por IdNecesidad, y luego devuelvo el primero de cada uno.
            // de esta manera elimino posibles duplicados
            return necesidades.GroupBy(x => x.IdNecesidad).Select(x => x.First());
        }

        public int GetTipoNecesidad(int idNecesidad)
        {
            IQueryable<Necesidades> query = _dbSet;

            return query.Where(x => x.IdNecesidad == idNecesidad).Select(x => x.TipoDonacion).FirstOrDefault();

        }
    }
}
