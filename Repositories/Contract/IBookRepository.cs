using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contract
{
    public interface IBookRepository
    {
        Task<Book?> GetOneBookAsync(bool isTrack, int id);
        Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack);
        Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book,bool>> expression);
        void InsertOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
