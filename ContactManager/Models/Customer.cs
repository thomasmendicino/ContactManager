using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class Customer : Contact
    {
		private string? _notes;

		public string? Notes
		{
			get { return _notes; }
			set { _notes = value; }
		}

		private Vendor? _vendor;

		public Vendor? Vendor
		{
			get { return _vendor; }
			set { _vendor = value; }
		}


	}
}
