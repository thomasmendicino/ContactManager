using ContactManager.DbContexts;
using ContactManager.DTOs;
using ContactManager.Models;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    /// <summary>
    /// Service for saving entities to the database.
    /// </summary>
    public class ContactCreator : IContactCreator
    {
        private readonly ContactManagerDbContextFactory _dbContextFactory;

        public ContactCreator(ContactManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Saves a customer to the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task CreateCustomer(Customer customer)
        {
            CustomerDTO customerDTO = MapCustomer(customer);

            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Customer.Add(customerDTO);

                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Saves a vendor to the database.
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task CreateVendor(Vendor vendor)
        {
            VendorDTO vendorDTO = MapVendor(vendor);

            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Vendor.Add(vendorDTO);

                dbContext.SaveChanges();
            }
        }

        public VendorDTO MapVendor(Vendor vendor)
        {
            return new VendorDTO
            {
                Name = vendor.Name,
                Phone = vendor.Phone,
                Address = vendor.Address,
                Company = vendor.Company
            };
        }

        public CustomerDTO MapCustomer(Customer customer)
        {
            return new CustomerDTO
            {
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address,
                Company = customer.Company,
                Notes = customer.Notes
            };
        }

        /// <summary>
        ///  Saves a vendor to the master list.
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task CreateVendorMasterRecord(Vendor vendor)
        {
            CompanyVendorDTO companyVendorDTO = MapCompanyVendor(vendor);

            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.VendorMasterList.Add(companyVendorDTO);

                dbContext.SaveChanges();
            }
        }

        public CompanyVendorDTO MapCompanyVendor(Vendor vendor)
        {
            return new CompanyVendorDTO
            {
                VendorCode = vendor.VendorCode,
                CompanyName = vendor.Company
            };
        }
    }
}
