using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactList
    {
        private IContactCreator _contactCreator;
        public ContactList(IContactCreator contactCreator)
        {
            _contactCreator = contactCreator;
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
    }
}
