using ContactManager.Models;
using ContactManager.Stores;
using ContactManager.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            return base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            Contact newContact;
            if (_addVendorViewModel != null)
            {
                if (!ValidateVendorInput(_addVendorViewModel))
                {
                    MessageBox.Show($"The provided vendor was invalid. Please enter a Name and Company at minimum.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

                    return;
                }
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
                    // a non-empty vendor code was provided.
                    // AND either VendorCode or CompanyName doesn't match the record that was found.
                    if (!string.IsNullOrEmpty(inputVendorCode) && 
                        !(inputVendorCode.Equals(existingVendor.VendorCode) &&
                        inputCompanyName.Equals(existingVendor.Company)))
                    {
                        MessageBox.Show($"The provided vendor code '{ inputVendorCode }' or company name '{ inputCompanyName }' belongs to another entry in the Vendor Master List. " +
                            Environment.NewLine + Environment.NewLine + $"Existing Vendor Code: '{ existingVendor.VendorCode }' and Company '{ existingVendor.Company }'." +
                            Environment.NewLine + Environment.NewLine + "Please enter a unique vendor code and company name, or use a previously saved company.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        
                        return;
                    }
                }
            }
            else
            {
                if (!ValidateCustomerInput(_addCustomerViewModel))
                {
                    MessageBox.Show($"The provided customer was invalid. Please enter a Name and Company at minimum.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

                    return;
                }
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
            catch (DbUpdateException)
            {
                MessageBox.Show("Duplicate vendor master list record. Vendor was saved, but master vendor list was not updated.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch(Exception e) {
                
                MessageBox.Show(string.Concat("Error saving contact: ",e.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateCustomerInput(AddCustomerViewModel? addCustomerViewModel)
        {
            return addCustomerViewModel != null && !string.IsNullOrWhiteSpace(addCustomerViewModel.Name) && !string.IsNullOrWhiteSpace(addCustomerViewModel.Company);
        }

        private bool ValidateVendorInput(AddVendorViewModel addVendorViewModel)
        {
            return addVendorViewModel != null && !string.IsNullOrWhiteSpace(addVendorViewModel.Name) && !string.IsNullOrWhiteSpace(addVendorViewModel.Company);
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
