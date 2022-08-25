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
        private readonly AddCustomerViewModel? _addCustomerViewModel;
        private readonly AddVendorViewModel? _addVendorViewModel;
        private readonly ContactList _contactList;
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public AddContactCommand(ContactList contactList, NavigationStore navigationStore, Func<ListContactsViewModel> createListContactsViewModel,
            AddCustomerViewModel? addCustomerViewModel, AddVendorViewModel? addVendorViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createListContactsViewModel;
            _addCustomerViewModel = addCustomerViewModel;
            _addVendorViewModel = addVendorViewModel;
            _contactList = contactList;
        }

        /*private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
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
        }*/

        public async override Task ExecuteAsync(object? parameter)
        {
            Contact newContact;
            if (_addVendorViewModel != null)
            {
                newContact = MapVendor(_addVendorViewModel);

                // await CheckForCompanyName();
                // do something.
            }
            else
            {
                newContact = MapCustomer(_addCustomerViewModel);
            }

            try 
            { 
                await _contactList.AddContact(newContact);
                
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch(Exception e) {
                
                MessageBox.Show(string.Concat("Error saving contact: ",e.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Contact MapCustomer(AddCustomerViewModel addCustomerViewModel)
        {
            return new Customer
            {
                Name = addCustomerViewModel.Name,
                Phone = addCustomerViewModel.Phone,
                Address = addCustomerViewModel.Address,
                Notes = addCustomerViewModel.Notes,
                Company = addCustomerViewModel.Company
            };
        }

        private Contact MapVendor(AddVendorViewModel addVendorViewModel)
        {
            return new Vendor
            {
                Name = addVendorViewModel.Name,
                Phone = addVendorViewModel.Phone,
                Address = addVendorViewModel.Address,
                Company = addVendorViewModel.Company,
                VendorCode = addVendorViewModel.VendorCode
            };
        }
    }
}
