using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceHistoryViewModel
    {
        private InvoiceModel _invoice;
        private InvoiceRepository invoiceRepository;
        public InvoiceModel Invoice { get; set; }
        public RelayCommand GetReceiptCommand { get; set; }
        public RelayCommand SearchItemCommand { get; set; }

        public InvoiceHistoryViewModel()
        {
            _invoice = new InvoiceModel();
            Invoice = new InvoiceModel();
            invoiceRepository = new InvoiceRepository();
            GetReceiptCommand = new RelayCommand(param => GetReceipt((InvoiceModel)param));
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
            GetAll();
        }

        private void GetAll()
        {
            _invoice.InvoiceRecords = invoiceRepository.GetAllInvoice();
            Invoice.InvoiceRecords = _invoice.InvoiceRecords;
        }

        private void GetReceipt(InvoiceModel invoice)
        {
            InvoiceReceiptView invoiceReceiptView = new InvoiceReceiptView(invoice);
            invoiceReceiptView.ShowDialog();
        }

        private void SearchItem(string searchTxt = "")
        {
            Invoice.InvoiceRecords = new ObservableCollection<InvoiceModel>(
                _invoice.InvoiceRecords.Where(i => i.InvoiceOrderRecords.Where(o => o.InvoiceId.ToString().Equals(searchTxt.ToLower())
                || o.Item.Name.ToLower().Contains(searchTxt.ToLower())
                || o.Item.Brand.ToLower().Contains(searchTxt.ToLower())).Any() == true).ToList());
        }
    }
}
