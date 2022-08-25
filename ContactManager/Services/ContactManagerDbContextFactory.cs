using ContactManager.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services
{
    public class ContactManagerDbContextFactory
    {
        private readonly string _connectionString;

        public ContactManagerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ContactManagerDbContext CreateDbContext()
        {
            DbContextOptions contextOptions = new DbContextOptionsBuilder().UseSqlServer().Options;

            ContactManagerDbContext dbContext = new ContactManagerDbContext(contextOptions);

            dbContext.Database.SetConnectionString(_connectionString);

            return dbContext;
        }
    }
}
