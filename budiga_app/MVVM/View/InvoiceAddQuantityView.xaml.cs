using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoiceAddQuantityView.xaml
    /// </summary>
    public partial class InvoiceAddQuantityView : Window
    {
        public int Quantity { get; set; }

        public InvoiceAddQuantityView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Quantity = Int32.Parse(pieceTextBox.Text) * Int32.Parse(quantityTextBox.Text);
            this.DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
