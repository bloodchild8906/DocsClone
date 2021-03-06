using DocsClone.Domain.Interfaces;
using DocsClone.EfCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.EfCore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context) => _context = context;
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).ToList();
        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
        public T GetById(int id) => _context.Set<T>().Find(id);
        public void Remove(T entity) => _context.Set<T>().Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

    }
}
