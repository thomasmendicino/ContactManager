using ContactManager.Commands;
using ContactManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        
        private readonly NavigationStore _navigationStore;
        public ICommand? AddCustomer { get; }
        public ICommand? Cancel { get; }

        public AddCustomerViewModel(NavigationStore navigationStore, Func<ListContactsViewModel> listContacts)
        {
            _navigationStore = navigationStore;

            AddCustomer = new NavigateCommand(navigationStore, listContacts);
            Cancel = new NavigateCommand(navigationStore, listContacts);
        }
    }
}
