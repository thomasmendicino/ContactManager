using ContactManager.Models;
using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Tests.Stubs
{
    internal class StubVendorCodeValidator : IVendorCodeValidator
    {
        public bool VendorExists { get; set; }
        public async Task<Vendor?> GetVendorFromMasterList(Vendor vendor)
        {
            if (VendorExists)
            {
                return vendor;
            }
            return null;
        }
    }
}
