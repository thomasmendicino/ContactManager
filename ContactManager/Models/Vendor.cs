using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
