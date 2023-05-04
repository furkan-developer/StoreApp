using AutoMapper;
using Entities;
using Entities.Dtos.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repositories;
using Services.Contract;
using Services.CustomExceptions;
using System.Linq.Expressions;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager repoManager, ILoggerService logger, IMapper mapper)
        {
            _repoManager = repoManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteOneBookAsync(int id)
        {

            var book = await _repoManager.BookRepository.GetOneBookAsync(false, id);
            if (book == null)
            {
                throw new BookNotFoundException("Not Found Book"); 
            }

            _repoManager.BookRepository.DeleteOneBook(book);
            await _repoManager.SaveChangesAsync();
            _repoManager.Commit(false);

        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack)
        {
            try
            {
                return await _repoManager.BookRepository.GetAllBooksAsync(isTrack);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when got list of the book object exception:\n{ex.Message}");
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            try
            {
                return await _repoManager.BookRepository.GetAllBooksAsync(isTrack,expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when got list of the book object exception:\n{ex.Message}");
            }
        }

        public async Task<Book> GetOneBookAsync(bool isTrack, int id)
        {
            try
            {
                 var book = await _repoManager.BookRepository.GetOneBookAsync(isTrack, id);

                if (book is null)
                    throw new FileNotFoundException("Not found book");

                return book;
            }catch (Exception ex)
            {
                throw new Exception($"Created exception when got book object exception:\n{ex.Message}");
            }
        }

        public async Task InsertOneBookAsync(BookDtoForInsert bookDto)
        {
            try
            {
                if (bookDto == null)
                    throw new ArgumentNullException("Book object is not null reference");

                var book = _mapper.Map<Book>(bookDto);

                _repoManager.BookRepository.InsertOneBook(book);
               await _repoManager.SaveChangesAsync();
                _repoManager.Commit(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when added book object exception:\n{ex.Message}");
            }
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto)
        {
            try
            {
                var result = await _repoManager.BookRepository.GetOneBookAsync(false, id);
                if (result is null)
                    throw new FileNotFoundException("Not found book");

                result = _mapper.Map<Book>(bookDto);

                _repoManager.BookRepository.UpdateOneBook(result);
                await _repoManager.SaveChangesAsync();
                _repoManager.Commit(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Created exception when updated book object exception:\n{ex.Message}");
            }
        }
    }
}