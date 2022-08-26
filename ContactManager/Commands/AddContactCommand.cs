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
using System.Windows.Navigation;

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
            if (_addVendorViewModel != null)
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return //!_duplicateCompany && !string.IsNullOrEmpty(_addVendorViewModel?.Company) &&
                base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            Contact newContact;
            if (_addVendorViewModel != null)
            {
                newContact = MapVendor(_addVendorViewModel);
                string inputVendorCode = _addVendorViewModel.VendorCode;
                string inputCompanyName = _addVendorViewModel.Company;

                Vendor existingVendor = await _contactList.GetVendorFromMasterList((Vendor)newContact);
                
                if (existingVendor == null && ValidVendorCode(inputCompanyName, inputVendorCode))
                {
                    _saveVendorCode = true;                    
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputVendorCode) && 
                        !(inputVendorCode.Equals(existingVendor.VendorCode) &&
                        inputCompanyName.Equals(existingVendor.Company)))
                    {
                        MessageBox.Show($"The provided vendor code '{ inputVendorCode }' or company name '{inputCompanyName}' belongs to another entry in the Vendor Master List. \nPlease enter a unique vendor code and company name, or use a previously saved company.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        
                        return;
                    }
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
                    await _contactList.AddVendorMasterRecord((Vendor)newContact);
                }                
                               
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Duplicate vendor master list record.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch(Exception e) {
                
                MessageBox.Show(string.Concat("Error saving contact: ",e.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidVendorCode(string inputCompanyName, string inputVendorCode)
        {
            return !string.IsNullOrEmpty(inputCompanyName) && !string.IsNullOrEmpty(inputVendorCode);
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
