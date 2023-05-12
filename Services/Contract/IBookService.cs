using Entities;
using Entities.Dtos.Book;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IBookService
    {
        Task<Book> GetOneBookAsync(bool isTrack, int id);
        Task<(IEnumerable<ExpandoObject> books,MetaData metaData)> GetAllBooksAsync(bool isTrack,BookRequestParameters requestParameters);
        Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression);
        Task InsertOneBookAsync(BookDtoForInsert book);
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto);
        Task DeleteOneBookAsync(int id);
    }
}
