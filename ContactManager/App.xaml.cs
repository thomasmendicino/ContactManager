using System;
using System.Windows;
using ContactManager.DbContexts;
using ContactManager.Enums;
using ContactManager.Models;
using ContactManager.Services;
using ContactManager.Stores;
using ContactManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ContactManagerDbContextFactory _dbContextFactory;
        private readonly ContactList _contactList;
        private readonly CompanyVendor _companyVendor;
        private readonly NavigationStore _navigationStore;
        private IConfiguration _configuration;
        private readonly DatabaseServer _dbProvider;

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            _configuration = builder.AddJsonFile("appsettings.json", false, true).Build();

            // use Sqlite as the default if provider missing.
            if (!Enum.TryParse(_configuration["DatabaseProvider"], out _dbProvider)) {
                _dbProvider = DatabaseServer.Sqlite;
            }

            if (_dbProvider.Equals(DatabaseServer.SqlServer))
            {
                // Sql Server connection string. Using sql server account
                _dbContextFactory = new ContactManagerDbContextFactory($"User ID ={_configuration["UserName"]}; Password={_configuration["Password"]};Initial Catalog = ContactManager; Server={_configuration["DatabaseServer"]}");

                _dbContextFactory.dbServer = _dbProvider;
            }
            else
            {
                // Sqlite connection string. No user/pw
                _dbContextFactory = new ContactManagerDbContextFactory($"Data Source = { _configuration["DatabaseServer"] }");

                _dbContextFactory.dbServer = _dbProvider;
            }
            // pass in db context factor to service interfaces
            IContactCreator contactCreator = new ContactCreator(_dbContextFactory);
            IContactRepository contactRepo = new ContactRepository(_dbContextFactory);
            IVendorCodeValidator vendorCodeValidator = new VendorCodeValidator(_dbContextFactory);
            
            // initial list of contacts.
            _contactList = new ContactList(contactCreator, contactRepo, vendorCodeValidator);
            _companyVendor = new CompanyVendor(contactRepo);

            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /** SQL SERVER VERSION  */
            if (_dbProvider.Equals(DatabaseServer.SqlServer))
            {
                using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    dbContext.Database.Migrate();
                }
            }
            /** END SQL SERVER VERSION **/

            /** SQLITE VERSION **/
            else
            {
                DbContextOptions dbContextOptions = new DbContextOptionsBuilder().UseSqlite($"Data Source = {_configuration["DatabaseServer"]}").Options;

                using (SQLiteContactManagerDbContext dbContext = new SQLiteContactManagerDbContext(dbContextOptions))
                {
                    dbContext.Database.Migrate();
                }
            }
            /** END SQLITE VERSION **/

            _navigationStore.CurrentViewModel = new ListContactsViewModel(_contactList, _navigationStore, CreateCustomerViewModel, CreateVendorViewModel, CreateVendorMasterListViewModel);

            MainWindow = new MainWindow {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
        // The following methods enable the navigation command to populate a new instance of the viewmodel for each page load.
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
