using ContactManager.Commands;
using ContactManager.Models;
using ContactManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ContactViewModel? Current { get; set; }

        public ListContactsViewModel(ContactList contactList, NavigationStore navigationStore, Func<AddCustomerViewModel> addCustomerViewModel, Func<AddVendorViewModel> addVendorViewModel)
        {
            AddCustomer = new AddContactCommand(this, contactList, navigationStore, addCustomerViewModel);
            //AddCustomer = new AddContactCommand(this, contactList, addCustomerViewModel);
            //AddCustomer = new NavigateCommand(navigationStore, addCustomerViewModel);
            //AddVendor = new NavigateCommand(navigationStore, addVendorViewModel);

            _contacts = new ObservableCollection<ContactViewModel>();
            _contactList = contactList;

            UpdateContactList();
            /*_contacts.Add(new ContactViewModel(new Customer() { Name="Thomas", Phone = "555-5555", Address = "1234 C Street"}));
            _contacts.Add(new ContactViewModel(new Customer() { Name = "Jonas", Phone = "555-5556", Address = "1234 D Street" }));
            _contacts.Add(new ContactViewModel(new Customer() { Name = "Jerry", Phone = "555-5557", Address = "1234 E Street" }));
            _contacts.Add(new ContactViewModel(new Vendor() { Name = "Jerry", Phone = "555-5557", Address = "1234 E Street" }));*/
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
