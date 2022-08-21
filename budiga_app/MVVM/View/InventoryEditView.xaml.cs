using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InventoryEditView.xaml
    /// </summary>
    public partial class InventoryEditView : Window
    {
        private InventoryViewModel viewModel;
        private ItemModel _item;

        public InventoryEditView(ItemModel item)
        {
            InitializeComponent();
            viewModel = InventoryViewModel.GetInstance;
            _item = item;
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
            qtyTextBlock.Text = (int.Parse(qtyTextBlock.Text) + int.Parse(qtyTextBox.Text)).ToString();
        }
        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(productTextBox.Text) || string.IsNullOrEmpty(brandTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text) || string.IsNullOrEmpty(qtyTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ItemModel item = new ItemModel()
                {
                    Id = _item.Id,
                    Name = productTextBox.Text,
                    Barcode = (string.IsNullOrEmpty(barcodeTextBox.Text)) ? "N/A" : barcodeTextBox.Text,
                    Brand = brandTextBox.Text,
                    Price = decimal.Parse(priceTextBox.Text),
                    Quantity = int.Parse(qtyTextBlock.Text)
                };
                if (await viewModel.UpdateItem(item, _item))
                {
                    this.Close();
                }
            }

        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Continue Action?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await viewModel.DeleteItem(_item))
                {
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
