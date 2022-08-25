namespace ContactManager.Models
{
    public abstract class Contact
    {
        private string _name;
        private string? _address;
        private string? _phone;
        private string company;

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

        public string Company
        {
            get { return company; }
            set { company = value; }
        }


        public override string ToString()
        {
            return this._name;
        }
    }
}
