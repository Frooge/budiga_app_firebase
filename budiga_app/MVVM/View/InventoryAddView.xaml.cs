using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for InventoryAddView.xaml
    /// </summary>
    public partial class InventoryAddView : Window
    {
        private InventoryViewModel _vm;
        public InventoryAddView(InventoryViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemModel item = new ItemModel()
            {
                Name = productTextBox.Text,
                Brand = brandTextBox.Text,
                Price = int.Parse(priceTextBox.Text),
                Quantity = int.Parse(qtyTextBox.Text)
            };
            ItemRepository itemRepository = new ItemRepository();
            if (itemRepository.AddItem(item))
            {
                _vm.GetAll();
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
