using ContactManager.Models;
using ContactManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactManager.Commands
{
    public class AddContactCommand : AsyncCommandBase
    {
        private readonly ManageContactsViewModel _manageContactsViewModel;
        private readonly ContactList _contactList;

        public AddContactCommand(ManageContactsViewModel manageContactsViewModel, ContactList contactList)
        {
            _manageContactsViewModel = manageContactsViewModel;
            _contactList = contactList;

            _manageContactsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactViewModel.Name))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return //!string.IsNullOrEmpty(_manageContactsViewModel.CurrentName) &&
                base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            Customer customer = new Customer() { Name = "Test", Company = "Company A" };

            try { await _contactList.AddContact(customer); }
            catch(Exception e) {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
