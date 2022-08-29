using ContactManager.DTOs;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IVendorCodeValidator
    {
        // fetch vendor based on supplied vendor code and company name
        public Task<Vendor?> GetVendorFromMasterList(Vendor vendor);

        // Cases:
        // 1. Vendor code was not supplied => do not save anything to master list.
        // 2. Company name/Vendor code search returned nothing => save vendor to master list
        // 3. Company name/Vendor code search returned an entry that matches both the supplied vendor code and company name => skip saving; already exists.
        // 4. Company name/Vendor code search returned an entry and one of the properties doesn't match the supplied value => Return Error box indicating why company
        //     will not be saved to master list.
    }
}
