using ContactManager.Commands;
using ContactManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ContactManager.ViewModels
{
    public class AddVendorViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ICommand AddVendor { get; }

        public AddVendorViewModel(NavigationStore navigationStore, Func<ListContactsViewModel> listContacts)
        {
            _navigationStore = navigationStore;

            AddVendor = new NavigateCommand(navigationStore, listContacts);
        }
    }
}
