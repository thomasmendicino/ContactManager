using ContactManager.DbContexts;
using ContactManager.Enums;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services
{
    public class ContactManagerDbContextFactory
    {
        private readonly string _connectionString;
        public DatabaseServer dbServer { get; set; }

        public ContactManagerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual ContactManagerDbContext CreateDbContext()
        {
            ContactManagerDbContext dbContext = null;
            switch (dbServer)
            {
                case DatabaseServer.SqlServer:
                    DbContextOptions contextOptions = new DbContextOptionsBuilder().UseSqlServer().Options;

                    dbContext = new ContactManagerDbContext(contextOptions);

                    dbContext.Database.SetConnectionString(_connectionString);
                    
                    break;
                case DatabaseServer.Sqlite:
                    DbContextOptions dbContextOptions = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

                    dbContext = new SQLiteContactManagerDbContext(dbContextOptions);

                    break;
            }            

            return dbContext;
        }
    }
}
