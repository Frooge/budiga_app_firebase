using budiga_app.Core;
using budiga_app.MVVM.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard.xaml
    /// </summary>
    public partial class EmployeeDashboard : Window
    {
        private DataClass dataClass;

        public EmployeeDashboard()
        {
            InitializeComponent();
            dataClass = DataClass.GetInstance;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += CheckInternetAvailability;
            timer.Start();
        }

        private void CheckInternetAvailability(object sender, EventArgs e)
        {
            if (!InternetAvailability.IsInternetAvailable())
            {
                MessageBox.Show("Device is not connected to the internet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            this.Close();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (await Logout())
            {
                GC.Collect(); // find finalizable objects
                GC.WaitForPendingFinalizers(); // wait until finalizers executed
                GC.Collect(); // collect finalized objects
            }

        }

        private async Task<bool> Logout()
        {
            bool result = false;
            if (await dataClass.Checkout())
            {
                DataClass.ReleaseInstance();
                InventoryViewModel.ReleaseInstance();
                InvoiceViewModel.ReleaseInstance();
                SalesViewModel.ReleaseInstance();
                EmployeeViewModel.ReleaseInstance();
                result = true;
            }
            return result;
        }
    }
}
