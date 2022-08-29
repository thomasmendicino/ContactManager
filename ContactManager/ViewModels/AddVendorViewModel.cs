using ContactManager.Commands;
using ContactManager.Models;
using ContactManager.Stores;
using System;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class AddVendorViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _company;

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        private string _vendorCode;

        public string VendorCode
        {
            get { return _vendorCode; }
            set { _vendorCode = value; }
        }
        private readonly NavigationStore _navigationStore;
        public ICommand SaveVendor { get; }
        public ICommand Cancel { get; }

        public AddVendorViewModel(ContactList contactList, NavigationStore navigationStore, Func<ListContactsViewModel> listContactsViewModel)
        {
            _navigationStore = navigationStore;

            SaveVendor = new AddContactCommand(contactList, navigationStore, listContactsViewModel, null, this);
            Cancel = new NavigateCommand(navigationStore, listContactsViewModel);
        }

        
    }
}
