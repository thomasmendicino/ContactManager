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
        private bool _duplicateCompany = true;
        private bool _saveVendorCode;

        public AddContactCommand(ContactList contactList, NavigationStore navigationStore, Func<ListContactsViewModel> createListContactsViewModel,
            AddCustomerViewModel? addCustomerViewModel, AddVendorViewModel? addVendorViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createListContactsViewModel;
            _addCustomerViewModel = addCustomerViewModel;
            _addVendorViewModel = addVendorViewModel;
            _contactList = contactList;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_addVendorViewModel != null && e.PropertyName == nameof(ContactViewModel.VendorCode))
            {
                // In order to allow submit, the following must be true
                // 1. Contact is type "customer" OR
                // 2a. company name is not a duplicate AND
                // 2b. vendor code is populated OR
                // 3a. company name is a duplicate AND
                // 3b. vendor code is not supplied.
                // So, we should disable the Submit button until and unless one of the above conditions is met.
                // check for duplicate.
                // since it is async, 
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return !_duplicateCompany && !string.IsNullOrEmpty(_addVendorViewModel?.Company) &&
                base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            Contact newContact;
            if (_addVendorViewModel != null)
            {
                newContact = MapVendor(_addVendorViewModel);

                string existingVendorCode = await _contactList.GetVendorCode((Vendor)newContact);
                
                if (!string.IsNullOrEmpty(existingVendorCode))
                {
                    if (!_addVendorViewModel.VendorCode.Equals(existingVendorCode))
                    {
                        MessageBox.Show($"Company already exists in Master Vendor List with Vendor Code {existingVendorCode}. The entered code will not be saved.", 
                            "Error", 
                            MessageBoxButton.OK, 
                            MessageBoxImage.Error);
                    }
                }
                else
                {
                    _saveVendorCode = true;
                }
                
            }
            else
            {
                newContact = MapCustomer(_addCustomerViewModel);
            }

            try 
            { 
                await _contactList.AddContact(newContact);

                if (_saveVendorCode) {
                    await _contactList.SaveVendorCode(newContact);
                }                
                               
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch (DuplicateVendorCodeException)
            {
                MessageBox.Show(, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
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
