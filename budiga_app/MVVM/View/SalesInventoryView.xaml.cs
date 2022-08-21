using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for SalesInventoryView.xaml
    /// </summary>
    public partial class SalesInventoryView : UserControl
    {
        public SalesViewModel ViewModel { get; set; }
        public SalesInventoryView()
        {
            ViewModel = SalesViewModel.GetInstance;
            InitializeComponent();

        }
    }
}
