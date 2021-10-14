using DocsClone.Domain.Interfaces;
using DocsClone.EfCore.Contexts;
using DocsClone.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.EfCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Revisions = new RevisionRepository(_context);
            Users = new UserRepository(_context);
            Documents = new DocumentRepository(_context);
            Details = new DetailRepository(_context);
        }
        public IRevisionRepository Revisions { get;}
        public IUserRepository Users { get;}
        public IDocumentRepository Documents { get;}
        public IDetailRepository Details { get;}
        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();

    }
}
