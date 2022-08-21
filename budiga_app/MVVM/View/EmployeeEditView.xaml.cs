using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeEditView.xaml
    /// </summary>
    public partial class EmployeeEditView : Window
    {
        public EmployeeViewModel ViewModel { get; set; }
        public UserModel User { get; set; }
        public EmployeeEditView(UserModel user)
        {
            ViewModel = EmployeeViewModel.GetInstance;
            User = user;
            InitializeComponent();
            var branch = ViewModel.Data.Store.BranchRecords.Where(s => s.Id == user.StoreId).FirstOrDefault();
            branchComboBox.ItemsSource = ViewModel.Data.Store.BranchRecords;
            branchComboBox.SelectedItem = branch;
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

        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(branchComboBox.Text) || string.IsNullOrEmpty(fNameTextBox.Text) || string.IsNullOrEmpty(lNameTextBox.Text) || string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(contactTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserModel user = new UserModel()
                {
                    Id = User.Id,
                    BranchId = ((StoreModel.BranchModel)branchComboBox.SelectedItem).Id,
                    FName = fNameTextBox.Text,
                    LName = lNameTextBox.Text,
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                    Contact = contactTextBox.Text,
                };
                UserRepository userRepository = new UserRepository();
                if (await userRepository.UpdateUser(user))
                {
                    this.Close();
                }
            }

        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            UserRepository userRepository = new UserRepository();
            if (await userRepository.DeleteUser(User.Id))
            {
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
