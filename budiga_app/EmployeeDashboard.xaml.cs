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
using System.Windows.Shapes;
using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard.xaml
    /// </summary>
    public partial class EmployeeDashboard : Window
    {
        private AttendanceModel attendance;
        private AttendanceRepository attendanceRepository;
        private DataClass dataClass;

        public EmployeeDashboard()
        {
            InitializeComponent();
            dataClass = DataClass.GetInstance;
            userName.Text = string.Format("{0} {1} | {2}", dataClass.LoggedInUser.FName, dataClass.LoggedInUser.LName, dataClass.LoggedInUser.Type);
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
            await Logout();
        }

        private async Task<bool> Logout()
        {
            bool result = false;
            if (await dataClass.Checkout())
            {
                DataClass.ReleaseInstance();
                InventoryViewModel.ReleaseInstance();
                InvoiceViewModel.ReleaseInstance();
                EmployeeViewModel.ReleaseInstance();
                result = true;
            }
            return result;
        }
    }
}
