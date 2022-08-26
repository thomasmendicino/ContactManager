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

        public async Task<string> GetVendorCode(Vendor vendor)
        {
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                var companyVendorDTO = await dbContext.VendorMasterList.FirstOrDefaultAsync(c => c.CompanyName == vendor.Company);
                
                return companyVendorDTO == null ? String.Empty : companyVendorDTO.VendorCode;
            }
        }
    }
}
