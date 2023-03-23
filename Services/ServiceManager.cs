using Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IBookService bookService)
        {
            _bookService = new Lazy<IBookService> (()=> bookService);
        }

        public IBookService BookService => _bookService.Value;
    }
}
