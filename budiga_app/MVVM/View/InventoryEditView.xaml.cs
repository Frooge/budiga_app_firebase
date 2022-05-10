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
    /// Interaction logic for InventoryEditView.xaml
    /// </summary>
    public partial class InventoryEditView : Window
    {
        private InventoryViewModel _vm;
        private int id;

        public InventoryEditView(InventoryViewModel vm, ItemModel item)
        {
            InitializeComponent();
            _vm = vm;
            id = item.Id;
            productTextBox.Text = item.Name;
            barcodeTextBox.Text = (item.Barcode != null) ? item.Barcode.ToString() : "N/A";
            brandTextBox.Text = item.Brand;
            priceTextBox.Text = item.Price.ToString();
            qtyTextBlock.Text = item.Quantity.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            qtyTextBlock.Text = (int.Parse(qtyTextBlock.Text)+int.Parse(qtyTextBox.Text)).ToString();
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(productTextBox.Text) || string.IsNullOrEmpty(brandTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text) || string.IsNullOrEmpty(qtyTextBox.Text))
            {             
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ItemModel item = new ItemModel()
                {
                    Id = id,
                    Barcode = (string.IsNullOrEmpty(barcodeTextBox.Text)) ? "N/A" : barcodeTextBox.Text,
                    Name = productTextBox.Text,
                    Brand = brandTextBox.Text,
                    Price = int.Parse(priceTextBox.Text),
                    Quantity = int.Parse(qtyTextBlock.Text)
                };
                ItemRepository itemRepository = new ItemRepository();
                if (itemRepository.UpdateItem(item))
                {
                    _vm.GetAll();
                    this.Close();
                }
            }
            
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
