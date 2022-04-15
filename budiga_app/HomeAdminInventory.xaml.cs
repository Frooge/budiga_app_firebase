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

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeAdminInventory : Window
    {
        public HomeAdminInventory()
        {
            InitializeComponent();
        }

        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            AdminDashboard stats = new AdminDashboard();
            stats.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            stats.Show();

            this.Close();
        }

        private void AnalyticBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            HomeAdminAnalytics ana = new HomeAdminAnalytics();
            ana.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ana.Show();

            this.Close();
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();

            //HomeAdminInventory invent = new HomeAdminInventory();
            //invent.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //invent.Show();

            //this.Close();
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            HomeAdminEmployee emp = new HomeAdminEmployee();
            emp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            emp.Show();

            this.Hide();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();

            this.Close();
        }

    }
}
