using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.DbContexts
{
    internal class ContactManagerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContactManagerDbContext>
    {
        public ContactManagerDbContext CreateDbContext(string[] args)
        {
            
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer().Options;

            return new ContactManagerDbContext(options);
            
        }
    }
}
