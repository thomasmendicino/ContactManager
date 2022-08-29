using ContactManager.Models;
using ContactManager.Views;


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
        public ContactType ContactType { get; set; }


        public ContactViewModel(Contact contact)
        {
            _contact = contact;
            
            if (contact is Customer)
            {
                _customer = (Customer)contact;
                ContactType = ContactType.Customer;
            }
                
            else if (contact is Vendor)
            {
                _vendor = (Vendor)contact;
                ContactType = ContactType.Vendor;
            }
                
        }
    }
}
