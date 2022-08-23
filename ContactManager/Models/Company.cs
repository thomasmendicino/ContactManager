namespace ContactManager.Models
{
    public class Company
    {
        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        private string _vendorCode;

        public string VendorCode
        {
            get { return _vendorCode; }
            set { _vendorCode = value; }
        }


        public Company()
        {

        }
    }
}