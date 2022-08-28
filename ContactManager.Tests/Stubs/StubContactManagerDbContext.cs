/*using ContactManager.DbContexts;
using ContactManager.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Stubs
{
    internal class StubContactManagerDbContext : ContactManagerDbContext
    {
        List<object> addedEntry = new List<object>();
        internal DbSet<CustomerDTO> Customer { get; set; }
        internal DbSet<VendorDTO> Vendor { get; set; }
        internal DbSet<CompanyVendorDTO> VendorMasterList { get; set; }
        internal StubContactManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public override EntityEntry Add(object entity)
        {
            addedEntry.Add(entity);

            return base.Add(entity);
        }

    }
}
*/