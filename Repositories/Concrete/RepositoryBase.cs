using Entities.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public abstract class RepositoryBase<T> : IReposityoryBase<T>
        where T : class, IEntity , new()
    {
        private readonly DbSet<T> _entities;

        protected RepositoryBase(RepositoryContext context)
        {
            _entities = context.Set<T>();  
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public IQueryable<T> Get(bool isTrack, int id) => 
            isTrack ? _entities.Where(x => x.Id == id) :
                _entities.Where(x => x.Id == id).AsNoTracking();

        public IQueryable<T> GetAll(bool isTrack) =>
            isTrack ? _entities.AsQueryable()
            : _entities.AsNoTracking();

        public IQueryable<T> GetAll(bool isTrack, Expression<Func<T, bool>> expression) =>
            isTrack ? _entities.Where(expression) 
            : _entities.Where(expression).AsNoTracking();

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
