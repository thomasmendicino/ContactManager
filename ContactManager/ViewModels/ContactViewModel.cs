using ContactManager.Commands;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        private readonly Contact _contact;

        public string Name => _contact.Name;
        public string Phone => _contact.Phone;
        public string Address => _contact.Address;

        public ContactViewModel(Contact contact)
        {
            _contact = contact;
        }
    }
}
