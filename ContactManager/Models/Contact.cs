using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactManager.Models
{
    public abstract class Contact
    {
        private string _name;
        private string _address;
        private string _phone;
        private string _company;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public string Phone { get; set; }

        public override string ToString()
        {
            return this._name;
        }
    }
}
