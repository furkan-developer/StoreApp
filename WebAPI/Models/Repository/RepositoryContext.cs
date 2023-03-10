using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.Repository
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
        :base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}