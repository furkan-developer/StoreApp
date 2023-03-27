using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomExceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(string message) : base(message)
        {
        }
    }
}
