using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for AnalyticsView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        public SalesViewModel ViewModel { get; set; }
        public SalesView()
        {
            ViewModel = SalesViewModel.GetInstance;
            InitializeComponent();
        }
    }
}
