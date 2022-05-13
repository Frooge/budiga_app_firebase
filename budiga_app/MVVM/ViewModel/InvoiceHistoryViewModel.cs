using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceHistoryViewModel
    {
        private InvoiceRepository invoiceRepository;
        public InvoiceModel Invoice { get; set; }
        
        public InvoiceHistoryViewModel()
        {
            Invoice = new InvoiceModel();
            invoiceRepository = new InvoiceRepository();            
            GetAll();
        }

        private void GetAll()
        {
            Invoice.InvoiceRecords = invoiceRepository.GetAllInvoice();
        }
    }
}
