using Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        public static IQueryable<Book> SearchBook(this IQueryable<Book> books, string titleSearchTerm)
        {
            if (string.IsNullOrWhiteSpace(titleSearchTerm))
                return books;

            string lowerCase = titleSearchTerm.Trim().ToLower();
            return books.Where(b => b.Title.ToLower().Contains(lowerCase));
        }
    }
}
