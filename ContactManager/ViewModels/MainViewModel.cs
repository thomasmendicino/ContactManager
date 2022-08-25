using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel {get;}

        public MainViewModel(ContactList contactList)
        {
            CurrentViewModel = new ManageContactsViewModel(contactList);
        }

    }
}
