using Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBook(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            return books.Where(b => (b.Price > minPrice) && (b.Price < maxPrice));
        }

        public static IQueryable<Book> SearchBook(this IQueryable<Book> books, string titleSearchTerm)
        {
            if (string.IsNullOrWhiteSpace(titleSearchTerm))
                return books;

            string lowerCase = titleSearchTerm.Trim().ToLower();
            return books.Where(b => b.Title.ToLower().Contains(lowerCase));
        }

        // orderByPattern Example: title desc, price
        public static IQueryable<Book> SortBooks(this IQueryable<Book> books,string orderByPattern)
        {
            if(string.IsNullOrWhiteSpace(orderByPattern))
                return books.OrderBy(b => b.Id);

            string orderByQuery = OrderQueryBuilder.CreateOrderQuery(orderByPattern);

            if(orderByQuery is null)
                return books.OrderBy(b=> b.Id);

            return books.OrderBy(orderByQuery);
        }
    }
}
