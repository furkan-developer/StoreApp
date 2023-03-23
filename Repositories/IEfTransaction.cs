using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEfTransaction
    {
        void Commit(bool callSaveChanges = true, bool rollBackTransaction = false);
    }
}
