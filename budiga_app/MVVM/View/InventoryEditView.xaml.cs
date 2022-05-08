using budiga_app.MVVM.Model;
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
        public InventoryEditView(ItemModel item)
        {
            InitializeComponent();
            productTextBox.Text = item.Name;
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
            qtyTextBlock.Text += qtyTextBox.Text;
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
