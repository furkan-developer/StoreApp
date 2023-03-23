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
        private IDbContextTransaction _dbContextTransaction;
        public RepositoryManager(RepositoryContext context,
            IBookRepository bookRepository)
        {
            _context = context;
            _dbContextTransaction = _context.Database.BeginTransaction();
            _bookRepository = new Lazy<IBookRepository>(() => bookRepository);
        }
        public IBookRepository BookRepository => _bookRepository.Value;

        public void Commit(bool rollBackTransaction = false, bool callSaveChanges = true)
        {
            if(callSaveChanges)
                SaveChanges();

            if (rollBackTransaction)
                _context.Database.RollbackTransaction();
            else
                _dbContextTransaction.Commit();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
