using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRevisionRepository Revisions { get;}
        IUserRepository Users { get;  }
        IDocumentRepository Documents { get;  }
        IDetailRepository Details { get; }
        int Complete();
    }
}
