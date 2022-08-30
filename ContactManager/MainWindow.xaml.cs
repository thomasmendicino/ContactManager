using System;
using System.Diagnostics;
using System.Windows;

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
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// Dispatcher timer runs on the UI thread with a lower priority* and is evaluated at the top of every Dispatcher loop. Since it may not be executed exactly at 0ms 
        /// of the next second, any delays will be accumulated by the next full 1 second delay. To address this, the current time is evaluated on each execution and subtracted 
        /// from the next interval to determine when the timer will fire next.
        /// * Priority: (4) Background: The enumeration value is 4. Operations are processed after all other non-idle operations are completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Clock.Text = now.ToString();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000 - now.Millisecond);
        }


    }
}
