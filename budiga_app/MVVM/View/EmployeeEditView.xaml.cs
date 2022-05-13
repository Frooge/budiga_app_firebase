using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeEditView.xaml
    /// </summary>
    public partial class EmployeeEditView : Window
    {
        private EmployeeViewModel _vm;
        private int userId;
        public EmployeeEditView(UserModel user, EmployeeViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            userId = user.Id;
            fNameTextBox.Text = user.FName;
            lNameTextBox.Text = user.LName;
            usernameTextBox.Text = user.Username;
            passwordTextBox.Text = user.Password;
            contactTextBox.Text = user.Contact;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fNameTextBox.Text) || string.IsNullOrEmpty(lNameTextBox.Text) || string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(contactTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserModel user = new UserModel()
                {
                    Id = userId,
                    FName = fNameTextBox.Text,
                    LName = lNameTextBox.Text,
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                    Contact = contactTextBox.Text,
                };
                UserRepository userRepository = new UserRepository();
                if (userRepository.UpdateUser(user))
                {
                    _vm.GetAllEmployee();
                    this.Close();
                }
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
