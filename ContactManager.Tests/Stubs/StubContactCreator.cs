using ContactManager.Models;
using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Stubs
{
    internal class StubContactCreator : IContactCreator
    {
        public List<Customer> customersAdded { get; set; } = new List<Customer>();
        public List<Vendor> vendorsAdded { get; set; } = new List<Vendor>();
        public List<Vendor> vendorMasterList { get; set; } = new List<Vendor>();
        public Task CreateCustomer(Customer customer)
        {
            customersAdded.Add(customer);
            return Task.CompletedTask;
        }

        public Task CreateVendor(Vendor vendor)
        {
            vendorsAdded.Add(vendor);
            return Task.CompletedTask;
        }

        public Task CreateVendorMasterRecord(Vendor vendor)
        {
            vendorMasterList.Add(vendor);
            return Task.CompletedTask;
        }
    }
}
