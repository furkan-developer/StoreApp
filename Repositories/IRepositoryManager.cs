﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Contract;

namespace Repositories
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepository { get; }
        Task SaveChangesAsync();
    }
}
