using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.Core;
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
using budiga_app.MVVM.ViewModel;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoicePayView.xaml
    /// </summary>
    public partial class InvoicePayView : Window
    {
        private InvoiceViewModel _vm;
        private InvoiceModel _invoice;
        private InvoiceRepository _invoiceRepository;
        private ItemHistoryRepository _itemHistory;
        private ItemRepository _itemRepository;
        public InvoicePayView(InvoiceViewModel vm, InvoiceModel invoice)
        {
            InitializeComponent();
            _vm = vm;
            _invoice = invoice;
            _invoiceRepository = new InvoiceRepository();
            _itemHistory = new ItemHistoryRepository();
            _itemRepository = new ItemRepository();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {           
            _invoice.CustomerPay = int.Parse(payTextBox.Text);
            _invoice.CustomerChange = _invoice.CustomerPay - _invoice.TotalPrice;
            if (_invoice.CustomerChange >= 0)
            {
                _invoice.UserId = Sessions.session.Id;
                _invoiceRepository.AddInvoice(_invoice);
                _invoice = _invoiceRepository.GetLastInvoice();
                foreach(OrderModel order in _invoice.InvoiceOrderRecords)
                {
                    _itemHistory.AddItemHistory(order.Item, "UPDATED");
                    order.Item.Quantity -= order.Quantity;
                    _itemRepository.UpdateItem(order.Item);
                }
                _vm.CancelOrder();
                InvoiceReceiptView invoiceReceiptView = new InvoiceReceiptView(_invoice);
                invoiceReceiptView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Payment is not enough!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
