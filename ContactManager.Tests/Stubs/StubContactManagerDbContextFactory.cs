using ContactManager.DbContexts;
using ContactManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Stubs
{
    public class StubContactManagerDbContextFactory : ContactManagerDbContextFactory
    {
        public StubContactManagerDbContext dbContext { get; set; }
        public DatabaseFacade _database { get; set; }
        public List<object> entryAdded;
        public StubContactManagerDbContextFactory(string connectionString) : base(connectionString)
        {
            
        }

        public override ContactManagerDbContext CreateDbContext() {
            
            var options = new DbContextOptionsBuilder<ContactManagerDbContext>().UseInMemoryDatabase(databaseName: "AutomatedTestingDatabase").Options;

            dbContext = new StubContactManagerDbContext(options);

            //dbContext.Database.Migrate();

            dbContext.EntryAdded += DbContext_EntryAdded;

            return dbContext;
        }

        private void DbContext_EntryAdded()
        {
            entryAdded.Add(dbContext.Database);
            _database = dbContext.Database;
        }

        public void OnEntryAdded()
        {

        }
    }
}
