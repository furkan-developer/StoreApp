using Entities;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using Repositories.Extensions;
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

        public async Task<PagedList<Book>> GetAllBooksAsync(bool isTrack,BookRequestParameters requestParameters)
        {
            var resources = await GetAll(isTrack)
                .FilterBook(requestParameters.MinPrice, requestParameters.MaxPrice)
                .SearchBook(requestParameters.TitleSearchTerm)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(resources, requestParameters.PageNumber,requestParameters.PageSize);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression)
        {
            return await GetAll(isTrack,expression).ToListAsync();
        }

        public async Task<Book?> GetOneBookAsync(bool isTrack,int id)
        {
            return await Get(isTrack, id).SingleOrDefaultAsync();
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
