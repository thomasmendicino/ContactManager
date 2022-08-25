﻿using ContactManager.Commands;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class ManageContactsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ContactViewModel> _contacts;

        public IEnumerable<ContactViewModel> Contacts => _contacts;
        private readonly ContactList _contactList;

        public ICommand? AddContact { get; }
        public ContactViewModel? Current { get; set; }

        public ManageContactsViewModel(ContactList contactList)
        {
            AddContact = new AddContactCommand(this, contactList);
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
