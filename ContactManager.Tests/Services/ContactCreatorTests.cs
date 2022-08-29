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
            // Determine pattern for mocking db context.            
        }

        public void MapCustomerDTO_Should_MapProperties()
        {
            ContactCreator contactCreator = new ContactCreator(_dbContextFactory);

            string name = "Customer Name";
            string phone = "555-5234";
            string address = "1234 A St";
            string notes = "Lorem ipsum";
            string company = "Bio-Rad";

            Customer customer = new Customer();
            customer.Name = name;
            customer.Phone = phone;
            customer.Address = address;
            customer.Notes = notes;
            customer.Company = company;

            CustomerDTO customerDTO = contactCreator.MapCustomer(customer);

            customerDTO.Name.Should().Be(name);
            customerDTO.Phone.Should().Be(phone);
            customerDTO.Address.Should().Be(address);
            customerDTO.Notes.Should().Be(notes);
            customerDTO.Company.Should().Be(company);
        }

        public void MapVendorDTO_Should_MapProperties()
        {
            ContactCreator contactCreator = new ContactCreator(_dbContextFactory);

            string name = "Customer Name";
            string phone = "555-5234";
            string address = "1234 A St";
            string company = "Bio-Rad";

            Vendor vendor = new Vendor();
            vendor.Name = name;
            vendor.Phone = phone;
            vendor.Address = address;
            vendor.Company = company;


            VendorDTO vendorDTO = contactCreator.MapVendor(vendor);

            vendorDTO.Name.Should().Be(name);
            vendorDTO.Phone.Should().Be(phone);
            vendorDTO.Address.Should().Be(address);
            vendorDTO.Company.Should().Be(company);
            
        }

        public void MapCompanyVendorDTO_Should_MapProperties()
        {
            ContactCreator contactCreator = new ContactCreator(_dbContextFactory);

            string name = "Customer Name";
            string vendorCode = "A1";

            Vendor vendor = new Vendor();
            vendor.Company = name;
            vendor.VendorCode = vendorCode;

            CompanyVendorDTO companyVendorDTO = contactCreator.MapCompanyVendor(vendor);

            companyVendorDTO.CompanyName.Should().Be(name);
            companyVendorDTO.VendorCode.Should().Be(vendorCode);
            

        }
    }
}
