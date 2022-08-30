using System;
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
        /// Dispatcher timer runs on a different thread with lower priority. Since it may not be executed exactly at 0ms of the next second, any delays will be accumulated by the next
        /// full 1 second delay. To address this, the current time is evaluated on each execution and subtracted from the next interval to determine when the timer will fire next.
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
