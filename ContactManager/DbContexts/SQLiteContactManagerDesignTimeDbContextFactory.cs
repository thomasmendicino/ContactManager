using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.DbContexts
{
    internal class SQLiteContactManagerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SQLiteContactManagerDbContext>
    {
        public SQLiteContactManagerDbContext CreateDbContext(string[] args)
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder().UseSqlite("Data Source=contactManager.db").Options;

            return new SQLiteContactManagerDbContext(dbContextOptions);
           
        }
    }
}
