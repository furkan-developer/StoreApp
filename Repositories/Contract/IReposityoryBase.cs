using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contract
{
    public interface IReposityoryBase<T>
    {
        //CRUD
        void Create(T entity);
        IQueryable<T> GetAll(bool isTrack);
        IQueryable<T> GetAll(bool isTrack, Expression<Func<T,bool>> expression);
        IQueryable<T> Get(bool isTrack, int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
