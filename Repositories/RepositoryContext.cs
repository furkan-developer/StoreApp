using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Book>(new BookEntityTypeConfiguration());
        }
        public DbSet<Book> Books { get; set; }
    }
}