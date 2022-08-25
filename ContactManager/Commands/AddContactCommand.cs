using ContactManager.Models;
using ContactManager.Stores;
using ContactManager.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly ListContactsViewModel _listContactsViewModel;
        private readonly ContactList _contactList;
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public AddContactCommand(ListContactsViewModel listContactsViewModel, ContactList contactList, 
            NavigationStore navigationStore, Func<ListContactsViewModel> createListContactsViewModel)
        {
            _listContactsViewModel = listContactsViewModel;
            _contactList = contactList;
            _navigationStore = navigationStore;
            _createViewModel = createListContactsViewModel;

            _listContactsViewModel.PropertyChanged += OnViewModelPropertyChanged;
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

            try 
            { 
                await _contactList.AddContact(customer);
                
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch(Exception e) {
                
                MessageBox.Show(string.Concat("Error saving contact: ",e.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
