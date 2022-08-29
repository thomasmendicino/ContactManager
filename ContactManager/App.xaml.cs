using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
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
        /*private const string CONNECTION_STRING = "Persist Security Info=False;User ID =wpfuser; Password=password1;Initial Catalog = ContactManager; Server=TOM-PC";
        private const string SQLITE_CONNECTION_STRING = "Data Source=contactManager.db";*/
        private readonly ContactManagerDbContextFactory _dbContextFactory;
        private readonly ContactList _contactList;
        private readonly CompanyVendor _companyVendor;
        private readonly NavigationStore _navigationStore;
        private IConfiguration _configuration;
        private readonly DatabaseServer _dbProvider;

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();//.SetBasePath() //.AddJsonFile.Add()("appsettings.json", false, true);
            _configuration = builder.AddJsonFile("appsettings.json", false, true).Build();
            /*
             "DatabaseProvider": "Sqlite",
  "DatabaseServer": "contactManager.db",
  "Username": "",
  "Password": ""
             
             
             */

            //_dbProvider = _configuration["DatabaseProvider"];

            if (!Enum.TryParse(_configuration["DatabaseProvider"], out _dbProvider)) {
                _dbProvider = DatabaseServer.Sqlite;
            }

            if (_dbProvider.Equals(DatabaseServer.SqlServer))
            {
                // Sql Server connection string
                _dbContextFactory = new ContactManagerDbContextFactory($"User ID ={_configuration["UserName"]}; Password={_configuration["Password"]};Initial Catalog = ContactManager; Server={_configuration["DatabaseServer"]}");

                _dbContextFactory.dbServer = _dbProvider;
            }
            else
            {
                // Sqlite connection string
                _dbContextFactory = new ContactManagerDbContextFactory($"Data Source = { _configuration["DatabaseServer"] }");

                _dbContextFactory.dbServer = _dbProvider;
            }            
            
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
