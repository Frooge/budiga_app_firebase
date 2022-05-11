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
        private int _id;
        private int _userId;
        private float _totalPrice;
        private float _customerPay;
        private float _customerChange;
        private DateTime _createdDate;
        private UserModel _user;
        private ObservableCollection<InvoiceModel> _invoiceRecords;
        private ObservableCollection<OrderModel> _invoiceOrderRecords;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int UserId { get { return _userId; } set { _userId = value; OnPropertyChanged("UserId"); } }
        public float TotalPrice { get { return _totalPrice; } set { _totalPrice = value; OnPropertyChanged("TotalPrice"); } }
        public float CustomerPay { get { return _customerPay; } set { _customerPay = value; OnPropertyChanged("CustomerPay"); } }
        public float CustomerChange { get { return _customerChange; } set { _customerChange = value; OnPropertyChanged("CustomerChange"); } }
        public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; OnPropertyChanged("CreatedDate"); } }
        public UserModel User { get { return _user; } set { _user = value; OnPropertyChanged("User"); } }
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
