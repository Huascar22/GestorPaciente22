using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Interfaces
{
    public interface IServicios<T> where T : class
    {
        Task Actualizar(T entidad, CancellationToken cancellationToken);
        Task Borrar(T entidad, CancellationToken cancellationToken);
        Task<T> Crear(T entidad, CancellationToken cancellationToken);
        Task<List<T>> Obtener(CancellationToken cancellationToken);
        Task<T> ObtenerById(int id, T entidad, CancellationToken cancellationToken);
        Task<T> ObtenerById(string id, T entidad, CancellationToken cancellationToken);
        IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includes);
    }
}
