using Entities;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public void DeleteOneBook(Book book)
        {
            Delete(book);
        }

        public IEnumerable<Book> GetAllBooks(bool isTrack)
        {
            return GetAll(isTrack).ToList();
        }

        public IEnumerable<Book> GetAllBooks(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            return GetAll(isTrack,expression).ToList();
        }

        public Book? GetOneBook(bool isTrack,int id)
        {
            return Get(isTrack, id).SingleOrDefault();
        }

        public void InsertOneBook(Book book)
        {
            Create(book);
        }

        public void UpdateOneBook(Book book)
        {
            Update(book);
        }
    }
}
