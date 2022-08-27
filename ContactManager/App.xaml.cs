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
        private const string SQLITE_CONNECTION_STRING = "Data Source=contactManager.db";
        private readonly ContactManagerDbContextFactory _dbContextFactory;
        private readonly ContactList _contactList;
        private readonly CompanyVendor _companyVendor;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            //SQL SERVER
            _dbContextFactory = new ContactManagerDbContextFactory(CONNECTION_STRING);
            
            IContactCreator contactCreator = new ContactCreator(_dbContextFactory);
            IContactRepository contactRepo = new ContactRepository(_dbContextFactory);
            IVendorCodeValidator vendorCodeValidator = new VendorCodeValidator(_dbContextFactory);
            // pass in db context to application.
            _contactList = new ContactList(contactCreator, contactRepo, vendorCodeValidator);
            _companyVendor = new CompanyVendor(contactRepo);

            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            /** SQL SERVER VERSION  **/
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            /** END SQL SERVER VERSION **/

            /** SQLITE VERSION 
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder().UseSqlite(SQLITE_CONNECTION_STRING, x => x.MigrationsAssembly("../SqliteMigrations")).Options;
                        
            using (ContactManagerDbContext dbContext = new ContactManagerDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
            /** END SQLITE VERSION **/
            _navigationStore.CurrentViewModel = new ListContactsViewModel(_contactList, _navigationStore, CreateCustomerViewModel, CreateVendorViewModel, CreateVendorMasterListViewModel);

            MainWindow = new MainWindow {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private AddVendorViewModel CreateVendorViewModel()
        {
            return new AddVendorViewModel(_contactList, _navigationStore, CreateContactListViewModel);
        }

        private AddCustomerViewModel CreateCustomerViewModel()
        {
            return new AddCustomerViewModel(_contactList, _navigationStore, CreateContactListViewModel);
        }

        private ListContactsViewModel CreateContactListViewModel()
        {
            return new ListContactsViewModel(_contactList, _navigationStore, CreateCustomerViewModel, CreateVendorViewModel, CreateVendorMasterListViewModel);
        }

        private VendorMasterListViewModel CreateVendorMasterListViewModel()
        {
            return new VendorMasterListViewModel(_companyVendor, _navigationStore, CreateContactListViewModel);
        }
    }
}
