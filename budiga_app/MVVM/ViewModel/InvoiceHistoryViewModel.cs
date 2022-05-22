using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
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
        private InvoiceRepository invoiceRepository;
        public InvoiceModel Invoice { get; set; }

        private InvoiceModel _invoice;

        public RelayCommand SearchItemCommand { get; set; }
        public InvoiceHistoryViewModel()
        {
            _invoice = new InvoiceModel();
            Invoice = _invoice;
            invoiceRepository = new InvoiceRepository();
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
            GetAll();
        }

        private void GetAll()
        {
            _invoice.InvoiceRecords = invoiceRepository.GetAllInvoice();
            Invoice.InvoiceRecords = _invoice.InvoiceRecords;
        }

        private void SearchItem(string searchTxt = "")
        {
            Invoice.InvoiceRecords = new ObservableCollection<InvoiceModel>(
                _invoice.InvoiceRecords.Where(i => i.InvoiceOrderRecords.Where(o => o.Item.Barcode.ToLower().Contains(searchTxt.ToLower()))
                || i.InvoiceOrderRecords.Where(o => o.Item.Name.ToLower().Contains(searchTxt.ToLower()))
                || i.InvoiceOrderRecords.Where(o => o.Item.Brand.ToLower().Contains(searchTxt.ToLower())));
        }
    }
}
