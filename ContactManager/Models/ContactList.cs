using ContactManager.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    /// <summary>
    /// Class to represent the collection of both vendors and customers. Provides methods for saving, retrieval, and validation.
    /// </summary>
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

        /// <summary>
        /// Saves a new contact entity to the database.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves all contacts from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactRepo.GetAllContacts();
        }

        /// <summary>
        /// Retrieves any matching vendors from the master vendor list based on vendor code or company name
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task<Vendor?> GetVendorFromMasterList(Vendor vendor)
        {
            return await _vendorCodeValidator.GetVendorFromMasterList(vendor);
        }        

        /// <summary>
        /// Saves a new record to the master vendor list
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task AddVendorMasterRecord(Vendor vendor)
        {
            await _contactCreator.CreateVendorMasterRecord(vendor);
        }
    }
}
