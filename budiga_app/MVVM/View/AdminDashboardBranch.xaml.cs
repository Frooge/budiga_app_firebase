using budiga_app.Core;
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
using budiga_app.MVVM.Model;

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
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
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
