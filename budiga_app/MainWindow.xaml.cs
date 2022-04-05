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

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void usernameBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            usernameBox.Focus();
        }

        private void usernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(usernameBox.Text) && usernameBox.Text.Length > 0)
            {
                usernameBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                usernameBlock.Visibility = Visibility.Visible;
            }
        }

        private void passwordBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                passwordBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordBlock.Visibility = Visibility.Visible;
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            HomeAdmin home = new HomeAdmin();
            home.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            home.Show();

            this.Close();
            
        }
    }
}
