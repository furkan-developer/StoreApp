using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Repository.Config;

namespace WebAPI.Models.Repository
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
        :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration<Book>(new BookEntityTypeConfiguration());
        }
        public DbSet<Book> Books { get; set; }
    }
}