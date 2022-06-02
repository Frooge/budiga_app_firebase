using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class InvoiceRepository
    {

        public InvoiceRepository()
        {

        }

        public ObservableCollection<InvoiceModel> GetAllInvoice()
        {
            ObservableCollection<InvoiceModel> invoiceRecords = new ObservableCollection<InvoiceModel>();
            
            return invoiceRecords;
        }

        public InvoiceModel GetLastInvoice()
        {
            InvoiceModel invoice = new InvoiceModel();
            
            return invoice;
        }

        public bool AddInvoice(InvoiceModel invoice)
        {
            bool result = false;
            
            return result;
        }
    }
}
