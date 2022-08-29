using ContactManager.Commands;
using ContactManager.Models;
using ContactManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class ListContactsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ContactViewModel> _contacts;

        public IEnumerable<ContactViewModel> Contacts => _contacts;
        private readonly ContactList _contactList;

        public ICommand? AddCustomer { get; }
        public ICommand? AddVendor { get; }
        public ICommand? MasterVendorList { get; }
        public ContactViewModel? Current { get; set; }

        public ListContactsViewModel(ContactList contactList, NavigationStore navigationStore, 
            Func<AddCustomerViewModel> addCustomerViewModel, 
            Func<AddVendorViewModel> addVendorViewModel,
            Func<VendorMasterListViewModel> vendorMasterListViewModel)
        {
            AddCustomer = new NavigateCommand(navigationStore, addCustomerViewModel);
            AddVendor = new NavigateCommand(navigationStore, addVendorViewModel);
            MasterVendorList = new NavigateCommand(navigationStore, vendorMasterListViewModel);

            _contacts = new ObservableCollection<ContactViewModel>();
            _contactList = contactList;

            UpdateContactList();
        }

        private async void UpdateContactList()
        {
            var contacts = await _contactList.GetAllContacts();
            foreach (var contact in contacts)
            {
                _contacts.Add(new ContactViewModel(contact));
            }
        }
    }
}
