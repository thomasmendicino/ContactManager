﻿using ContactManager.DbContexts;
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
    public class ContactRepository : IContactRepository
    {
        private readonly ContactManagerDbContextFactory _dbContextFactory;

        public ContactRepository(ContactManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CustomerDTO> customerDTOs = await dbContext.Customers.ToListAsync();
                IEnumerable<VendorDTO> vendorDTOs = await dbContext.Vendors.ToListAsync();
                
                IEnumerable<Contact> contacts = null;
                IEnumerable<Contact> vendors = null;
                if (customerDTOs != null)
                    contacts = customerDTOs.Select(c => MapCustomerDTO(c));
                if (vendorDTOs != null)
                    vendors = vendorDTOs.Select(v => MapVendorDTO(v));

                return CombineContacts(vendors, contacts);
            }
        }

        private IEnumerable<Contact> CombineContacts(IEnumerable<Contact> vendors, IEnumerable<Contact> contacts)
        {
            if (contacts == null) return vendors;
            else if (vendors == null) return contacts;
            return contacts.Concat(vendors);
        }

        private Contact MapCustomerDTO(CustomerDTO customerDTO)
        {
            return new Customer
            {
                Name = customerDTO.Name,
                Phone = customerDTO.Phone,
                Address = customerDTO.Address,
                Company = customerDTO.Company,
                Notes = customerDTO.Notes
            };
        }

        private Contact MapVendorDTO(VendorDTO vendorDTO)
        {
            return new Vendor
            {
                Name = vendorDTO.Name,
                Phone = vendorDTO.Phone,
                Address = vendorDTO.Address,
                Company = vendorDTO.Company//,
                //VendorCode = vendorDTO.VendorCode
            };
        }
 
    }
}