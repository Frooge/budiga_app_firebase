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
        private int _id;
        private int _itemId;
        private int _invoiceId;
        private int _quantity;
        private float _subtotalPrice;
        private ItemModel _item;
        private ObservableCollection<OrderModel> _orderTransactionRecords;
        
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        public int InvoiceId { get { return _invoiceId; } set { _invoiceId = value; OnPropertyChanged("InvoiceId"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public float SubtotalPrice { get { return _subtotalPrice; } set { _subtotalPrice = value; OnPropertyChanged("SubtotalPrice"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }                
    }
}
