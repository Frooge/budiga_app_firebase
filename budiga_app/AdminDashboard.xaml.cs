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
using budiga_app.Core;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
            userName.Text = Sessions.session.Username;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Sessions.Dispose();
            this.Hide();
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            this.Close();
        }
    }
}
