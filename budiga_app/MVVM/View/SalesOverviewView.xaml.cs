using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for SalesOverviewView.xaml
    /// </summary>
    public partial class SalesOverviewView : UserControl
    {
        public SalesViewModel ViewModel { get; set; }
        public SalesOverviewView()
        {
            ViewModel = SalesViewModel.GetInstance;
            InitializeComponent();
        }

    }
}
