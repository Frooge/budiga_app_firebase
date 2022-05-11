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

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoicePayView.xaml
    /// </summary>
    public partial class InvoicePayView : Window
    {
        private InvoiceModel _invoice;
        private InvoiceRepository invoiceRepository;
        public InvoicePayView(InvoiceModel invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            invoiceRepository = new InvoiceRepository();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            _invoice.UserId = Sessions.session.Id;
            _invoice.CustomerPay = int.Parse(payTextBox.Text);
            _invoice.CustomerChange = _invoice.CustomerPay - _invoice.TotalPrice;
            invoiceRepository.AddInvoice(_invoice);
            _invoice = new InvoiceModel();
            InvoiceReceiptView invoiceReceiptView = new InvoiceReceiptView(invoiceRepository.GetLastInvoice());
            invoiceReceiptView.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
