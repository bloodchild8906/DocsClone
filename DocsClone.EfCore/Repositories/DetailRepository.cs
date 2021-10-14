using DocsClone.Domain.Entities;
using DocsClone.Domain.Interfaces;
using DocsClone.EfCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.EfCore.Repositories
{
    public class DetailRepository : GenericRepository<Detail>, IDetailRepository
    {
        public DetailRepository(ApplicationContext context) : base(context) => Expression.Empty();

        
    }
}
