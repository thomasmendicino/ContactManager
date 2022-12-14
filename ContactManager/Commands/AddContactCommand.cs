using ContactManager.Models;
using ContactManager.Stores;
using ContactManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace ContactManager.Commands
{
    /// <summary>
    /// Commands for saving entities when buttons are clicked.
    /// </summary>
    public class AddContactCommand : AsyncCommandBase
    {
        private readonly AddCustomerViewModel? _addCustomerViewModel;
        private readonly AddVendorViewModel? _addVendorViewModel;
        private readonly ContactList _contactList;
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;
        private bool _saveVendorCode;

        /// <summary>
        /// ContactList will contain the services to interact with the database. A mechanism for navigating to the home page is provided, and the appropriate
        /// view model depending on if customers or vendors are being added.
        /// </summary>
        /// <param name="contactList"></param>
        /// <param name="navigationStore"></param>
        /// <param name="createListContactsViewModel"></param>
        /// <param name="addCustomerViewModel"></param>
        /// <param name="addVendorViewModel"></param>
        public AddContactCommand(ContactList contactList, NavigationStore navigationStore, Func<ListContactsViewModel> createListContactsViewModel,
            AddCustomerViewModel? addCustomerViewModel, AddVendorViewModel? addVendorViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createListContactsViewModel;
            _addCustomerViewModel = addCustomerViewModel;
            _addVendorViewModel = addVendorViewModel;
            _contactList = contactList;
        }
        /// <summary>
        /// No logic for disabling/enabling buttons at this time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Saves a new contact to the database as a command.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
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

                // Cases:
                // 1. Vendor code was not supplied => do not save anything to master list.
                // 2. Company name/Vendor code search returned nothing => save vendor to master list
                // 3. Company name/Vendor code search returned an entry that matches both the supplied vendor code and company name => skip saving; already exists.
                // 4. Company name/Vendor code search returned an entry and one of the properties doesn't match the supplied value => Return Error box indicating why company
                //     will not be saved to master list.
                if (ValidVendorCode(inputVendorCode))
                {
                    Vendor existingVendor = await _contactList.GetVendorFromMasterList((Vendor)newContact);

                    if (existingVendor == null)
                    {
                        _saveVendorCode = true;
                    }
                    else
                    {
                        // a non-empty vendor code was provided.
                        // AND either VendorCode or CompanyName doesn't match the record that was found.
                        if (!(inputVendorCode.Equals(existingVendor.VendorCode) &&
                            inputCompanyName.Equals(existingVendor.Company)))
                        {
                            MessageBox.Show($"The provided vendor code '{inputVendorCode}' or company name '{inputCompanyName}' belongs to another entry in the Vendor Master List. " +
                                Environment.NewLine + Environment.NewLine + $"Existing Vendor Code: '{existingVendor.VendorCode}' and Company '{existingVendor.Company}'." +
                                Environment.NewLine + Environment.NewLine + "Please enter a unique vendor code and company name, or use a previously saved company.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                            return;
                        }
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

        private bool ValidVendorCode(string inputVendorCode)
        {
            return !string.IsNullOrEmpty(inputVendorCode);
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
