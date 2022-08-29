using ContactManager.DTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManager.DbContexts
{
    public class ContactManagerDbContext : DbContext
    {
        public ContactManagerDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<CustomerDTO> Customer { get; set; }
        public virtual DbSet<VendorDTO> Vendor { get; set; }
        public virtual DbSet<CompanyVendorDTO> VendorMasterList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyVendorDTO>(entity => {
                entity.HasIndex(v => v.VendorCode).IsUnique();
                entity.HasIndex(c => c.CompanyName).IsUnique();
            });

            modelBuilder.Entity<CompanyVendorDTO>().HasData(
                new CompanyVendorDTO { Id = Guid.NewGuid(), CompanyName = "ACME Acids", VendorCode = "A001" },
                new CompanyVendorDTO { Id = Guid.NewGuid(), CompanyName = "Berenstain Biology", VendorCode = "A002" },
                new CompanyVendorDTO { Id = Guid.NewGuid(), CompanyName = "Flick’s Fluidics", VendorCode = "A003" },
                new CompanyVendorDTO { Id = Guid.NewGuid(), CompanyName = "Radical Reagents", VendorCode = "D004" },
                new CompanyVendorDTO { Id = Guid.NewGuid(), CompanyName = "BBST Paper Products", VendorCode = "G065" });

            modelBuilder.Entity<CustomerDTO>().HasData(
                new CustomerDTO { Id = Guid.NewGuid(), Name = "Scottie Scheffler", Company = "Google", Phone = "(510)555-3282", Address = "1 Amphitheater Parkway, Mountain View, CA", Notes = "Interested in ddPCR." },
                new CustomerDTO { Id = Guid.NewGuid(), Name = "Warren Buffett", Company = "Berkshire Hathaway", Phone = "(510)555-8164", Address = "3555 Farnam Street, Omaha, NE", Notes = "Deep pockets." },
                new CustomerDTO { Id = Guid.NewGuid(), Name = "Elon Musk", Company = "Tesla", Phone = "(510)555-8912", Address = "13101 Harold Green Road, Austin, Texas", Notes = "Also deep pockets." });

            modelBuilder.Entity<VendorDTO>().HasData(
                new VendorDTO { Id = Guid.NewGuid(), Name = "George W. Salesman", Company = "Radical Reagents", Phone = "(510)555-1234", Address = "111 A Street, Benicia, CA" },
                new VendorDTO { Id = Guid.NewGuid(), Name = "Stephanie Saleswoman", Company = "Berenstain Biology", Phone = "(510)555-3289", Address = "222 B Street, Benicia, CA" },
                new VendorDTO { Id = Guid.NewGuid(), Name = "Jane Doe", Company = "ACME Acids", Phone = "(510)555-5439", Address = "333 C Street, Benicia, CA" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
