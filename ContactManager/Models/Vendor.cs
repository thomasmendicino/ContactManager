
namespace ContactManager.Models
{
    public class Vendor : Contact
    {
        private string? _vendorCode;

        public string? VendorCode
        {
            get { return _vendorCode; }
            set { _vendorCode = value; }
        }
    }
}
