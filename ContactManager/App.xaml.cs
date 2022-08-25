using System;
using System.Windows;
using ContactManager.DbContexts;
using ContactManager.Models;
using ContactManager.Services;
using ContactManager.Stores;
using ContactManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Persist Security Info=False;User ID =wpfuser; Password=password1;Initial Catalog = ContactManager; Server=TOM-PC";
        private readonly ContactManagerDbContextFactory _dbContextFactory;
        private readonly ContactList _contactList;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _dbContextFactory = new ContactManagerDbContextFactory(CONNECTION_STRING);

            IContactCreator contactCreator = new ContactCreator(_dbContextFactory);
            IContactRepository contactRepo = new ContactRepository(_dbContextFactory);
            // pass in db context to application.
            _contactList = new ContactList(contactCreator, contactRepo);

            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = new ListContactsViewModel(_contactList, _navigationStore, CreateCustomerViewModel, CreateVendorViewModel);

            MainWindow = new MainWindow {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private AddVendorViewModel CreateVendorViewModel()
        {
            return new AddVendorViewModel(_navigationStore, CreateContactListViewModel);
        }

        private AddCustomerViewModel CreateCustomerViewModel()
        {
            return new AddCustomerViewModel(_navigationStore, CreateContactListViewModel);
        }

        private ListContactsViewModel CreateContactListViewModel()
        {
            return new ListContactsViewModel(_contactList, _navigationStore, CreateCustomerViewModel, CreateVendorViewModel);
        }
    }
}
