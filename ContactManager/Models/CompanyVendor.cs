using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
	public class CompanyVendor
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

		private readonly IContactRepository _contactRepo;
        // parameterless ctor to only use this class for its properties
		//public CompanyVendor() {}
        public CompanyVendor(IContactRepository contactRepo)
		{
			_contactRepo = contactRepo;
		}

		public async Task<IEnumerable<Vendor>> GetCompanyVendorList()
        {
			return await _contactRepo.GetCompanyVendorList();
		}
	}
}
