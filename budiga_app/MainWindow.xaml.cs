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
using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;
using budiga_app.MVVM.ViewModel;
using budiga_app.MVVM.View;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginViewModel ViewModel { get; set; }
        public MainWindow()
        {
            ViewModel = new LoginViewModel();
            InitializeComponent();
        }

        private void usernameBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            usernameBox.Focus();
        }

        private void usernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(usernameBox.Text) && usernameBox.Text.Length > 0)
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
            var check = InternetAvailability.IsInternetAvailable();

            if (InternetAvailability.IsInternetAvailable())
            {
                RunQueryLogin();
            }
            else
            {
                MessageBox.Show("Device is not connected to the internet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RunQueryLogin()
        {
            try
            {
                ViewModel.Page.IsLoading = true;
                FirestoreConn conn = FirestoreConn.GetInstance;
                DataClass dataClass = DataClass.GetInstance;
                Query query = conn.FirestoreDb.Collection("Users").WhereEqualTo("Username", usernameBox.Text.Trim()).WhereEqualTo("Password", passwordBox.Password.Trim().ToString());
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                if (querySnapshot.Documents.Count > 0)
                {
                    Dictionary<string, object> dict = querySnapshot.Documents[0].ToDictionary();
                    dataClass.LoggedInUser = new UserModel
                    {
                        Id = dict["Id"].ToString(),
                        StoreId = dict["StoreId"].ToString(),
                        BranchId = dict["BranchId"].ToString(),
                        FName = dict["FName"].ToString(),
                        LName = dict["LName"].ToString(),
                        Username = dict["Username"].ToString(),
                        Password = dict["Password"].ToString(),
                        Contact = dict["Contact"].ToString(),
                        Type = dict["Type"].ToString(),
                    };
                    bool set = true;
                    if(!await dataClass.SetStore()) { set = false; }
                    if (!await dataClass.CheckIn()) { set = false; }

                    #region
                    if (dataClass.LoggedInUser.Type == "admin" && set)
                    {
                        this.Hide();
                        AdminDashboardBranch store = new AdminDashboardBranch
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        store.Show();
                        this.Close();
                    }
                    else if (dataClass.LoggedInUser.Type == "employee" && set)
                    {
                        this.Hide();
                        EmployeeDashboard employeeDashboard = new EmployeeDashboard
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        employeeDashboard.Show();
                        this.Close();
                    }
                    #endregion
                }
                else
                {
                    //Validation
                    #region
                    if (usernameBox.Text.Trim() == "" || passwordBox.Password.Trim().ToString() == "")
                    {
                        MessageBox.Show("Incomplete Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ViewModel.Page.IsLoading = false;
            }
        }
    }
}
