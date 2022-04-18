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
using MySql.Data.MySqlClient;

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
            runQueryLogin();
        }

        private void runQueryLogin()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=budiga_app;";
            string query = "SELECT * FROM users WHERE username = '" +usernameBox.Text.Trim()+ "' AND password = '" +passwordBox.Password.Trim().ToString() +"'";
            

            MySqlConnection dbconn = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, dbconn);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
           
            try
            {
                dbconn.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                { 
                    var results = new object[reader.FieldCount];
                    while (reader.Read())
                    {
                        reader.GetValues(results);
                    }
                    results.ToArray();
                    if(results.Length > 0)
                    {
                        if(results[6].ToString() == "Admin")
                        {
                            this.Hide();
                            AdminDashboard home = new AdminDashboard();
                            home.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            home.Show();
                            this.Close();
                        }
                        else
                        {
                            this.Hide();
                            EmployeeDashboard employeeDashboard = new EmployeeDashboard();
                            employeeDashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            employeeDashboard.Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    if(usernameBox.Text.Trim() == "" || passwordBox.Password.Trim().ToString() == "")
                    {
                        MessageBox.Show("Incomplete Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
