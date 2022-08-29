using Microsoft.EntityFrameworkCore;

namespace ContactManager.DbContexts
{
    public class SQLiteContactManagerDbContext : ContactManagerDbContext
    {
        public SQLiteContactManagerDbContext(DbContextOptions options) : base(options)
        {

        }        
    }
}
