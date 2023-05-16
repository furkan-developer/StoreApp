using AutoMapper;
using Entities;
using Entities.Dtos.Book;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repositories;
using Services.Contract;
using Services.CustomExceptions;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq.Expressions;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<Book> _dataShaper;

        public BookManager(IRepositoryManager repoManager, ILoggerService logger, IMapper mapper, IDataShaper<Book> dataShaper)
        {
            _repoManager = repoManager;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        public async Task DeleteOneBookAsync(int id)
        {

            var book = await GetOneBookByIdAndCheckExist(false, id);

            _repoManager.BookRepository.DeleteOneBook(book);
            await _repoManager.SaveChangesAsync();
        }

        public async Task<(IEnumerable<ExpandoObject>,MetaData)> GetAllBooksAsync(bool isTrack,BookRequestParameters requestParameters)
        {
            var pagedList = await _repoManager.BookRepository.GetAllBooksAsync(isTrack,requestParameters);

            var resources = _mapper.Map<IEnumerable<Book>>(pagedList);

            var shapedResources = _dataShaper.ShapeData(resources, requestParameters.Fields);

            return (shapedResources, pagedList.MetaData);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            return await _repoManager.BookRepository.GetAllBooksAsync(isTrack, expression);
        }

        public async Task<Book> GetOneBookAsync(bool isTrack, int id)
        {
            var book = await GetOneBookByIdAndCheckExist(false,id);

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

            var book = await GetOneBookByIdAndCheckExist(false, id);

            book = _mapper.Map<Book>(bookDto);

            _repoManager.BookRepository.UpdateOneBook(book);
            await _repoManager.SaveChangesAsync();
        }

        public async Task<bool> HasOneBookAsync(int id)
        {
            return await _repoManager.BookRepository.HasOneBookAsync(id);
        }

        #region Business Rules
        private async Task<Book> GetOneBookByIdAndCheckExist(bool isTrack, int id)
        {
            var result = await _repoManager.BookRepository.GetOneBookAsync(isTrack, id);
            if (result is null)
                throw new BookNotFoundException("Not found book");

            return result;
        }
        #endregion
    }
}