using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IBookService
    {
        Book GetOneBook(bool isTrack, int id);
        IEnumerable<Book> GetAllBooks(bool isTrack);
        IEnumerable<Book> GetAllBooks(bool isTrack, Expression<Func<Book, bool>> expression);
        void InsertOneBook(Book book);
        void UpdateOneBook(int id, Book book);
        void DeleteOneBook(int id);
    }
}
