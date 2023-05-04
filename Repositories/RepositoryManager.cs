using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        public Lazy<IBookRepository> _bookRepository;
        private RepositoryContext _context;
        public RepositoryManager(RepositoryContext context,
            IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => bookRepository);
        }
        public IBookRepository BookRepository => _bookRepository.Value;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
