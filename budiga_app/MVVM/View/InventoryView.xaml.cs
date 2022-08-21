using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{

    public partial class InventoryView : UserControl
    {
        public InventoryViewModel ViewModel { get; set; }
        public InventoryView()
        {
            ViewModel = InventoryViewModel.GetInstance;
            InitializeComponent();
        }
    }
}
