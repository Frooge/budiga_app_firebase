using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class InvoiceModel : ObservableObject
    {
        private string _id;
        private string _userFullName;
        //private string _storeId;
        //private string _branchId;
        private string _branchName;
        private string _address;
        private decimal _totalPrice;
        private decimal _customerPay;
        private decimal _customerChange;
        private DateTime _createdDate;
        private ObservableCollection<InvoiceModel> _invoiceRecords;
        private ObservableCollection<OrderModel> _invoiceOrderRecords;

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string UserFullName { get { return _userFullName; } set { _userFullName = value; OnPropertyChanged("UserFullName"); } }
        //public string StoreId { get { return _storeId; } set { _storeId = value; OnPropertyChanged("StoreId"); } }
        //public string BranchId { get { return _branchId; } set { _branchId = value; OnPropertyChanged("BranchId"); } }
        public string BranchName { get { return _branchName; } set { _branchName = value; OnPropertyChanged("BranchName"); } }
        public string Address { get { return _address; } set { _address = value; OnPropertyChanged("Address"); } }
        public decimal TotalPrice { get { return _totalPrice; } set { _totalPrice = value; OnPropertyChanged("TotalPrice"); } }
        public decimal CustomerPay { get { return _customerPay; } set { _customerPay = value; OnPropertyChanged("CustomerPay"); } }
        public decimal CustomerChange { get { return _customerChange; } set { _customerChange = value; OnPropertyChanged("CustomerChange"); } }
        public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; OnPropertyChanged("CreatedDate"); } }
        public ObservableCollection<InvoiceModel> InvoiceRecords { get { return _invoiceRecords; } set { _invoiceRecords = value; OnPropertyChanged("InvoiceRecords"); } }
        public ObservableCollection<OrderModel> InvoiceOrderRecords { get { return _invoiceOrderRecords; } set { _invoiceOrderRecords = value; OnPropertyChanged("InvoiceOrderRecords"); } }
        private void InvoiceRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("InvoiceRecords");
        }
        private void InvoiceOrderRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("InvoiceOrderRecords");
        }
    }
}
