using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IContactCreator
    {
        Task CreateCustomer(Customer customer);

        Task CreateVendor(Vendor vendor);

        Task CreateVendorMasterRecord(Vendor vendor);
    }
}
