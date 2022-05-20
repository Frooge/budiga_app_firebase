using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class SalesModel : ObservableObject
    {
        private int _id;
        private int _itemId;
        private int _invoiceId;
        private int _unitsSold;
        private float _subtotalPrice;
        private ItemModel _item;
        private ObservableCollection<SalesModel> _orderTransactionRecords;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        public int InvoiceId { get { return _invoiceId; } set { _invoiceId = value; OnPropertyChanged("InvoiceId"); } }
        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public float SubtotalPrice { get { return _subtotalPrice; } set { _subtotalPrice = value; OnPropertyChanged("SubtotalPrice"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
    }
}
