using budiga_app.Core;
using budiga_app.MVVM.Model;
using System.Windows;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for AdminDashboardStore.xaml
    /// </summary>
    public partial class AdminDashboardBranch : Window
    {
        DataClass dataClass;
        public AdminDashboardBranch()
        {
            dataClass = DataClass.GetInstance;
            InitializeComponent();
            branchComboBox.ItemsSource = dataClass.Store.BranchRecords;
            branchComboBox.SelectedValue = dataClass.Store.BranchRecords[0].Id;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(branchComboBox.Text))
            {
                MessageBox.Show("Select a branch!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                dataClass.Store.Branch = (StoreModel.BranchModel)branchComboBox.SelectedItem;
                AdminDashboard adminDashboard = new AdminDashboard
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                adminDashboard.Show();
                this.Close();
            }

        }
    }
}
