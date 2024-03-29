﻿using budiga_app.Core;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InventoryAddView.xaml
    /// </summary>
    public partial class InventoryAddView : Window
    {
        private InventoryViewModel viewModel;
        public InventoryAddView()
        {
            viewModel = InventoryViewModel.GetInstance;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(productTextBox.Text) || string.IsNullOrEmpty(brandTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text) || string.IsNullOrEmpty(qtyTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ItemModel item = new ItemModel()
                {
                    Id = GenerateId.GenerateCommon(),
                    Name = productTextBox.Text,
                    Barcode = (string.IsNullOrEmpty(barcodeTextBox.Text)) ? "N/A" : barcodeTextBox.Text,
                    Brand = brandTextBox.Text,
                    Price = decimal.Parse(priceTextBox.Text),
                    Quantity = int.Parse(qtyTextBox.Text)
                };
                if (await viewModel.AddItem(item))
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
