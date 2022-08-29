using ContactManager.Commands;
using ContactManager.Models;
using ContactManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class VendorMasterListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CompanyVendorViewModel> _vendorMasterList;

        public IEnumerable<CompanyVendorViewModel> VendorList => _vendorMasterList;
        public ICommand? Cancel { get; }

        private CompanyVendor _companyVendor;

        public VendorMasterListViewModel(CompanyVendor companyVendor, NavigationStore navigationStore, Func<ListContactsViewModel> listContactsViewModel)
        {
            Cancel = new NavigateCommand(navigationStore, listContactsViewModel);
            _companyVendor = companyVendor;
            _vendorMasterList = new ObservableCollection<CompanyVendorViewModel>();

            UpdateVendorMasterList();
        }

        private async void UpdateVendorMasterList()
        {
            IEnumerable<Vendor> companyVendors = await _companyVendor.GetCompanyVendorList();

            foreach(var cvItem in companyVendors)
            {
                _vendorMasterList.Add(new CompanyVendorViewModel(cvItem));
            }
        }
    }
}
