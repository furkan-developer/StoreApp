using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Contract;
using System.Linq.Expressions;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repoManager;

        public BookManager(IRepositoryManager repoManager)
        {
            _repoManager = repoManager;
        }

        public void DeleteOneBook(int id)
        {

            try
            {
                var book = _repoManager.BookRepository.GetOneBook(false,id);
                if(book == null) 
                    throw new FileNotFoundException("Not found book");

                _repoManager.BookRepository.DeleteOneBook(book);
                _repoManager.SaveChanges();

            }catch (Exception ex)
            {
                throw new Exception($"Created exception when deleted Book object exception:\n{ex.Message}");
            }
        }

        public IEnumerable<Book> GetAllBooks(bool isTrack)
        {
            try
            {
                return _repoManager.BookRepository.GetAllBooks(isTrack);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when got list of the book object exception:\n{ex.Message}");
            }
        }

        public IEnumerable<Book> GetAllBooks(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            try
            {
                return _repoManager.BookRepository.GetAllBooks(isTrack,expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when got list of the book object exception:\n{ex.Message}");
            }
        }

        public Book GetOneBook(bool isTrack, int id)
        {
            try
            {
                 var book = _repoManager.BookRepository.GetOneBook(isTrack, id);

                if (book is null)
                    throw new FileNotFoundException("Not found book");

                return book;
            }catch (Exception ex)
            {
                throw new Exception($"Created exception when got book object exception:\n{ex.Message}");
            }
        }

        public void InsertOneBook(Book book)
        {
            try
            {
                if (book == null)
                    throw new ArgumentNullException("Book object is not null reference");

                _repoManager.BookRepository.InsertOneBook(book);
                _repoManager.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when added book object exception:\n{ex.Message}");
            }
        }

        public void UpdateOneBook(int id, Book book)
        {
            try
            {
                var result = _repoManager.BookRepository.GetOneBook(false, id);
                if (result is null)
                    throw new FileNotFoundException("Not found book");

                result.Title = book.Title;
                result.Price = book.Price;

                _repoManager.BookRepository.UpdateOneBook(result);
                _repoManager.SaveChanges();

            }catch (Exception ex)
            {
                throw new Exception($"Created exception when updated book object exception:\n{ex.Message}");
            }
        }
    }
}