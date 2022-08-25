using System.Windows;
using ContactManager.DbContexts;
using ContactManager.Models;
using ContactManager.Services;
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

        public App()
        {
            _dbContextFactory = new ContactManagerDbContextFactory(CONNECTION_STRING);

            IContactCreator contactCreator = new ContactCreator(_dbContextFactory);
            // pass in db context to application.
            _contactList = new ContactList(contactCreator);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ContactManagerDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            MainWindow = new MainWindow {
                DataContext = new MainViewModel(_contactList)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
