using ContactManager.Models;

namespace ContactManager.ViewModels
{
    public class CompanyVendorViewModel : ViewModelBase
    {
        private readonly Vendor _vendor;

        public string VendorListVendorCode => _vendor.VendorCode;
        public string VendorListCompanyName => _vendor.Company;
        public CompanyVendorViewModel(Vendor vendor)
        {
            _vendor = vendor;
        }
    }
}