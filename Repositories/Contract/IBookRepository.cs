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
        Book? GetOneBook(bool isTrack, int id);
        IEnumerable<Book> GetAllBooks(bool isTrack);
        IEnumerable<Book> GetAllBooks(bool isTrack, Expression<Func<Book,bool>> expression);
        void InsertOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
