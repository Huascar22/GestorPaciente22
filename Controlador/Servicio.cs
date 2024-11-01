using Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class Servicios<T> : IServicios<T> where T : class
    {
        private readonly IRepositorio<T> _repositorio;
        public Servicios(IRepositorio<T> _repositorio)
        {
            this._repositorio = _repositorio;
        }
        public Task Actualizar(T entidad, CancellationToken cancellationToken)
        {
            return _repositorio.Actualizar(entidad, cancellationToken);
        }

        public Task Borrar(T entidad, CancellationToken cancellationToken)
        {
            return _repositorio.Borrar(entidad, cancellationToken);
        }

        public Task<T> Crear(T entidad, CancellationToken cancellationToken)
        {
            return _repositorio.Crear(entidad, cancellationToken);
        }

        public IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includes)
        {
            return _repositorio.GetWithInclude(includes);
        }

        public virtual Task<List<T>> Obtener(CancellationToken cancellationToken)
        {
            return _repositorio.Obtener(cancellationToken);
        }

        public Task<T> ObtenerById(int id, T entidad, CancellationToken cancellationToken)
        {
            return _repositorio.ObtenerById(id, entidad, cancellationToken);
        }
        public Task<T> ObtenerById(string id, T entidad, CancellationToken cancellationToken)
        {
            return _repositorio.ObtenerById(id, entidad, cancellationToken);
        }
    }
}
