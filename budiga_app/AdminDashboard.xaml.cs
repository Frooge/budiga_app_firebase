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
using System.Windows.Shapes;
using System.Windows.Threading;
using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using budiga_app.MVVM.ViewModel;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private DataClass dataClass;

        public AdminDashboard()
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

        private async void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            if(await Logout())
            {
                this.Close();
                GC.Collect(); // find finalizable objects
                GC.WaitForPendingFinalizers(); // wait until finalizers executed
                GC.Collect(); // collect finalized objects
            }            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private async Task<bool> Logout()
        {
            bool result = false;
            if(await dataClass.Checkout())
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            InventoryViewModel.ReleaseInstance();
            InvoiceViewModel.ReleaseInstance();
            SalesViewModel.ReleaseInstance();
            EmployeeViewModel.ReleaseInstance();

            this.Hide();
            AdminDashboardBranch store = new AdminDashboardBranch
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            store.Show();
            this.Close();
        }
    }
}
