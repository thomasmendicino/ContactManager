using ContactManager.DbContexts;
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
    public class StubContactManagerDbContext : ContactManagerDbContext
    {
        List<object> addedEntry = new List<object>();
        public override DbSet<CustomerDTO> Customer { get; set; }
        public override DbSet<VendorDTO> Vendor { get; set; }
        public override DbSet<CompanyVendorDTO> VendorMasterList { get; set; }
        internal StubContactManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public override EntityEntry Add(object entity)
        {
            addedEntry.Add(entity);

            OnEntryAdded();

            return base.Add(entity);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            addedEntry.Add(entity);

            OnEntryAdded();

            return base.Add(entity);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        private void OnEntryAdded()
        {
            EntryAdded?.Invoke();
        }

        public event Action EntryAdded;

    }
}
