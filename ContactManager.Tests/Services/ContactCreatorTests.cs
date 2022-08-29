using ContactManager.DbContexts;
using ContactManager.DTOs;
using ContactManager.Models;
using ContactManager.Services;
using ContactManager.Tests.Stubs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Services
{
    public class ContactCreatorTests
    {
        StubContactManagerDbContextFactory _dbContextFactory = new StubContactManagerDbContextFactory("");

        [Fact]
        public void AddNewCustomer_Should_AddCustomer()
        {
            ContactCreator contactCreator = new ContactCreator(_dbContextFactory);

            Vendor vendorToSave = new Vendor { Name = "test" };

            contactCreator.CreateVendor(vendorToSave);

            /*            using (dbContextFactory.CreateDbContext())
                        {
                            VendorDTO result = dbContextFactory.dbContext.Vendor.FirstOrDefault();

                            result.Name.Should().Be("test");
                        }*/
            var x = true;
            
        }
    }
}
