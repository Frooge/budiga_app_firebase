using budiga_app.MVVM.ViewModel;
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
