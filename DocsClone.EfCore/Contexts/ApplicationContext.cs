using DocsClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocsClone.EfCore.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<DocumentPermission> Permissions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
