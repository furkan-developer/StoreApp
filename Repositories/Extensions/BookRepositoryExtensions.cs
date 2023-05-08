using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBook(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            return books.Where(b => (b.Price > minPrice) && (b.Price < maxPrice));
        }
    }
}
