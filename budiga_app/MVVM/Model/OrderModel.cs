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
    public class OrderModel : ObservableObject
    {
        private string _id;
        private string _itemId;
        private string _invoiceId;
        private int _quantity;
        private decimal _actualItemPrice;
        private decimal _subtotalPrice;
        private ItemModel _item;
        private ObservableCollection<OrderModel> _orderRecords;
        
        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        public string InvoiceId { get { return _invoiceId; } set { _invoiceId = value; OnPropertyChanged("InvoiceId"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public decimal ActualItemPrice { get { return _actualItemPrice; } set { _actualItemPrice = value; OnPropertyChanged("ActualItemPrice"); } }
        public decimal SubtotalPrice { get { return _subtotalPrice; } set { _subtotalPrice = value; OnPropertyChanged("SubtotalPrice"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }     
        public ObservableCollection<OrderModel> OrderRecords { get { return _orderRecords; } set { _orderRecords = value; OnPropertyChanged("OrderRecords"); } }
        private void OrderRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("OrderRecords");
        }
    }
}
