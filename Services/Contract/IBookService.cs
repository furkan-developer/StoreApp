using Entities;
using Entities.Dtos.Book;
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
        Task<Book> GetOneBookAsync(bool isTrack, int id);
        Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack);
        Task<IEnumerable<Book>> GetAllBooksAsync(bool isTrack, Expression<Func<Book, bool>> expression);
        Task InsertOneBookAsync(BookDtoForInsert book);
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto);
        Task DeleteOneBookAsync(int id);
    }
}
