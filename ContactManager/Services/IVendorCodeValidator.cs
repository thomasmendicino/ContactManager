﻿using ContactManager.DTOs;
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
        // fetch company name
        public Task<Vendor?> GetVendorFromMasterList(Vendor vendor);

        // check if company name exists

        // check if vendor code exists

        // check if company name exists and vendor code does not match
        // 1. Company exists and already has another vendor code.
        // 1a. Company exists but vendor code was left blank.
        // 2. Company does not exist but vendor code belongs to another company.
        // 3. Company exists and vendor code matches.
        // 4. Company does not exist, vendor code doesn't exist.
    }
}
