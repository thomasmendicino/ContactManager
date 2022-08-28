using ContactManager.DbContexts;
using ContactManager.Services;
using Microsoft.EntityFrameworkCore;
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
        public ContactManagerDbContext dbContext { get; set; }
        public StubContactManagerDbContextFactory(string connectionString) : base(connectionString)
        {
            
        }

        public override ContactManagerDbContext CreateDbContext() {
            
            var options = new DbContextOptionsBuilder<ContactManagerDbContext>().UseInMemoryDatabase(databaseName: "AutomatedTestingDatabase").Options;

            dbContext = new ContactManagerDbContext(options);

            return dbContext;
        }
    }
}
