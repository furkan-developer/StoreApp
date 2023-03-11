using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Models.Repository.Config
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                 new Book(){Id=1,Title="ASP.NET Core with Web API",Price=Convert.ToDecimal(45.99)},
                new Book(){Id=2,Title="ASP.NET Core with WPF",Price=Convert.ToDecimal(58)},
                new Book(){Id=3,Title="ASP.NET Core with Web Application",Price=Convert.ToDecimal(48.49)}
            );
        }
    }
}