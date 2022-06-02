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
        private InventoryViewModel _vm;
        public InventoryHistoryView(InventoryViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _vm.Initialize();
        }
    }
}
