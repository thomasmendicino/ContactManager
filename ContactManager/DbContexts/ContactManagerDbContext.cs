﻿using ContactManager.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.DbContexts
{
    public class ContactManagerDbContext : DbContext
    {
        public ContactManagerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<VendorDTO> Vendors { get; set; }
        public DbSet<CompanyVendorDTO> VendorMasterList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyVendorDTO>(entity => {
                entity.HasIndex(v => v.VendorCode).IsUnique();
                entity.HasIndex(c => c.CompanyName).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
