using budiga_app.MVVM.ViewModel;
using System;
using System.Windows;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InventoryHistory.xaml
    /// </summary>
    public partial class InventoryHistoryView : Window
    {
        public InventoryViewModel ViewModel { get; set; }
        public InventoryHistoryView()
        {
            ViewModel = InventoryViewModel.GetInstance;
            if (ViewModel.CloseHistoryAction == null)
                ViewModel.CloseHistoryAction = new Action(this.Close);
            InitializeComponent();
        }
    }
}
