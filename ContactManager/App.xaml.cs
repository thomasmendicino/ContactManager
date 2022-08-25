using System.Windows;
using ContactManager.DbContexts;
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
        private readonly ContactManagerDbContextFactory _contactManagerDbContextFactory;

        public App()
        {
            _contactManagerDbContextFactory = new ContactManagerDbContextFactory(CONNECTION_STRING);

            // pass in db context to application.
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ContactManagerDbContext dbContext = _contactManagerDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            MainWindow = new MainWindow {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
