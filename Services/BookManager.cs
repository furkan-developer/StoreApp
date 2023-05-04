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
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack)
        {
            return await _repoManager.BookRepository.GetAllBooksAsync(isTrack);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            return await _repoManager.BookRepository.GetAllBooksAsync(isTrack, expression);
        }

        public async Task<Book> GetOneBookAsync(bool isTrack, int id)
        {
            var book = await _repoManager.BookRepository.GetOneBookAsync(isTrack, id);

            if (book is null)
                throw new BookNotFoundException("Not found book");

            return book;
        }

        public async Task InsertOneBookAsync(BookDtoForInsert bookDto)
        {

            if (bookDto == null)
                throw new ArgumentNullException("Book object is not null reference");

            var book = _mapper.Map<Book>(bookDto);

            _repoManager.BookRepository.InsertOneBook(book);
            await _repoManager.SaveChangesAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto)
        {

            var result = await _repoManager.BookRepository.GetOneBookAsync(false, id);
            if (result is null)
                throw new BookNotFoundException("Not found book");

            result = _mapper.Map<Book>(bookDto);

            _repoManager.BookRepository.UpdateOneBook(result);
            await _repoManager.SaveChangesAsync();
        }
    }
}