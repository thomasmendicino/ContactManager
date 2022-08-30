using ContactManager.DbContexts;
using ContactManager.DTOs;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class VendorCodeValidator : IVendorCodeValidator
    {
        private readonly ContactManagerDbContextFactory _dbContextFactory;

        public VendorCodeValidator(ContactManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        ///  Returns the first item found matching either the vendor code or the company name from the vendor master list.
        ///  If either matches, the caller can assume it isn't safe to save the current vendor to the master list.
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task<Vendor?> GetVendorFromMasterList(Vendor vendor)
        {
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                // Return vendor code and company name from any match of vendor code or company name.
                var companyVendorDTO = await dbContext.VendorMasterList.FirstOrDefaultAsync(v => v.VendorCode == vendor.VendorCode || v.CompanyName == vendor.Company);
                
                return companyVendorDTO == null ? null : new Vendor { VendorCode = companyVendorDTO.VendorCode, Company = companyVendorDTO.CompanyName };
            }
        }
    }
}
