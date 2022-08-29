using ContactManager.Commands;
using ContactManager.Models;
using ContactManager.Stores;
using System;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
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
        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public ICommand? SaveCustomer { get; }
        public ICommand? Cancel { get; }

        public AddCustomerViewModel(ContactList contactList, NavigationStore navigationStore, Func<ListContactsViewModel> listContactsViewModel)
        {
            SaveCustomer = new AddContactCommand(contactList, navigationStore, listContactsViewModel, this, null);
            Cancel = new NavigateCommand(navigationStore, listContactsViewModel);
        }
    }
}
