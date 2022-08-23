namespace ContactManager.Models
{
    public abstract class Contact
    {
        private string? _name;
        private string? _address;
        private string? _phone;
        private Company? _company;

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

        public string Phone 
        { 
            get 
            { 
                return this._phone; 
            } 
            set 
            { 
                this._phone = value; 
            } 
        }

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }


        public override string? ToString()
        {
            return this._name;
        }
    }
}
