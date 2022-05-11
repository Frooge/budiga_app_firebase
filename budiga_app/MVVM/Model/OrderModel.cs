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
        private int _orderId;
        private int _quantity;
        private int _subtotalPrice;
        private ItemModel _item;
        private ObservableCollection<OrderModel> _orderTransactionRecords;
        
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        public int OrderId { get { return _orderId; } set { _orderId = value; OnPropertyChanged("PurchaseId"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public int SubtotalPrice { get { return _subtotalPrice; } set { _subtotalPrice = value; OnPropertyChanged("SubtotalPrice"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
        public ObservableCollection<OrderModel> OrderTransactionRecords { get { return _orderTransactionRecords; } set { _orderTransactionRecords = value; OnPropertyChanged("OrderTransactionRecords"); } }
        private void OrderTransactionRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("OrderTransactionRecords");
        }
    }
}
