using ContactManager.DbContexts;
using ContactManager.DTOs;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ContactCreator : IContactCreator
    {
        private readonly ContactManagerDbContextFactory _dbContextFactory;

        public ContactCreator(ContactManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateCustomer(Customer customer)
        {
            CustomerDTO customerDTO = MapCustomer(customer);

            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Customers.Add(customerDTO);

                dbContext.SaveChanges();
            }
        }

        public async Task CreateVendor(Vendor vendor)
        {
            VendorDTO vendorDTO = MapVendor(vendor);

            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Vendors.Add(vendorDTO);

                dbContext.SaveChanges();
            }
        }

        private VendorDTO MapVendor(Vendor vendor)
        {
            return new VendorDTO
            {
                Name = vendor.Name,
                Phone = vendor.Phone,
                Address = vendor.Address,
                Company = vendor.Company
            };
        }

        private CustomerDTO MapCustomer(Customer customer)
        {
            return new CustomerDTO
            {
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address,
                Company = customer.Company
            };
        }

        
    }
}
