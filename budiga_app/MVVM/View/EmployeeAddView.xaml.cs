using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeAddView.xaml
    /// </summary>
    public partial class EmployeeAddView : Window
    {
        public EmployeeViewModel ViewModel { get; set; }
        public EmployeeAddView()
        {
            ViewModel = EmployeeViewModel.GetInstance;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(branchComboBox.Text) || string.IsNullOrEmpty(fNameTextBox.Text) || string.IsNullOrEmpty(lNameTextBox.Text) || string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(contactTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserModel user = new UserModel()
                {
                    FName = fNameTextBox.Text,
                    LName = lNameTextBox.Text,
                    StoreId = ViewModel.Data.Store.Id,
                    BranchId = ((StoreModel.BranchModel)branchComboBox.SelectedItem).Id,
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                    Contact = contactTextBox.Text,
                    Type = "employee",
                    Online = false,
                };
                UserRepository userRepository = new UserRepository();
                if (await userRepository.AddEmployeeUser(user))
                {
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
