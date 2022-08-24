using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow //: Window
    {
        private System.Windows.Threading.DispatcherTimer _dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
            // Set the clock value immediately to the current time.
            this.Clock.Text = DateTime.Now.ToString();
            this.Loaded += MainWindow_Loaded;
            //DataContext = new ContactManagerViewModel();
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            /* if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
                txtName.Clear();
            } */
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Clock.Text = now.ToString();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000 - now.Millisecond);
        }


    }
}
