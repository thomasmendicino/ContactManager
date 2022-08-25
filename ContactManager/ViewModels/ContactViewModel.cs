using ContactManager.Commands;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        private readonly Contact _contact;
        private readonly Customer? _customer;
        private readonly Vendor? _vendor;

        public string Name => _contact.Name;
        public string Phone => _contact.Phone;
        public string Address => _contact.Address;
        public string Company => _contact.Company;
        public string VendorCode => _vendor?.VendorCode ?? "";
        public string Notes => _customer?.Notes ?? "";
        public string ContactType { get; set; }


        public ContactViewModel(Contact contact)
        {
            _contact = contact;
            ContactType = contact.GetType().ToString();
            if (contact is Customer)
                _customer = (Customer)contact;
            else if (contact is Vendor)
                _vendor = (Vendor)contact;
        }
    }
}
