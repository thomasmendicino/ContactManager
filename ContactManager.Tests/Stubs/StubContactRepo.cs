using ContactManager.Models;
using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Stubs
{
    internal class StubContactRepo : IContactRepository
    {
        public List<Customer> customersToGet { get; set; }
        public List<Vendor> vendorsToGet { get; set; }
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            List<Contact> allContacts = new List<Contact>();
            
            if (customersToGet != null)
                allContacts.AddRange(customersToGet);
            if (vendorsToGet != null)
                allContacts.AddRange(vendorsToGet);

            return allContacts;
        }

        public async Task<IEnumerable<Vendor>> GetCompanyVendorList()
        {
            return vendorsToGet;
        }
    }
}
