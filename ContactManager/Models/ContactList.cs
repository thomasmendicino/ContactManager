using ContactManager.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactList
    {
        private IContactCreator _contactCreator;
        private IContactRepository _contactRepo;
        private IVendorCodeValidator _vendorCodeValidator;

        public ContactList(IContactCreator contactCreator, IContactRepository contactRepo, IVendorCodeValidator vendorCodeValidator)
        {
            _contactCreator = contactCreator;
            _contactRepo = contactRepo;
            _vendorCodeValidator = vendorCodeValidator;
        }

        public async Task AddContact(Contact contact)
        {
            if (contact is Customer)
            {
                await _contactCreator.CreateCustomer((Customer)contact);
            }
            else if (contact is Vendor)
            {
                await _contactCreator.CreateVendor((Vendor)contact);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactRepo.GetAllContacts();
        }

        public async Task<string> GetVendorCode(Vendor vendor)
        {
            return await _vendorCodeValidator.GetVendorCode(vendor);
        }
    }
}
