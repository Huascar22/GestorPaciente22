using Microsoft.EntityFrameworkCore;
using Modelo.AppContext;
using Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ContextoApp _context;
        public Repositorio(ContextoApp _context)
        {
            this._context = _context;
        }
        public async Task Actualizar(T entidad, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entidad);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Borrar(T entidad, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entidad);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> Crear(T entidad, CancellationToken cancellationToken)
        {
            _context.Set<T>().Add(entidad);
            await _context.SaveChangesAsync(cancellationToken);
            return entidad;
        }

        public IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task<List<T>> Obtener(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> ObtenerById(int id, T entidad, CancellationToken cancellationToken)
        {
            T? entitie = await _context.Set<T>().FindAsync(id);
            return entitie is null ? entidad : entitie;
        }

        public async Task<T> ObtenerById(string id, T entidad, CancellationToken cancellationToken)
        {
            T? entitie = await _context.Set<T>().FindAsync(id);
            return entitie is null ? entidad : entitie;
        }
    }
}
