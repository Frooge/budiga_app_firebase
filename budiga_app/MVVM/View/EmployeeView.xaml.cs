using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public EmployeeViewModel ViewModel { get; set; }
        public EmployeeView()
        {
            ViewModel = EmployeeViewModel.GetInstance;
            InitializeComponent();
        }
    }
}
