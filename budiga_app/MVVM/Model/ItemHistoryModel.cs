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
    public class ItemHistoryModel : ObservableObject
    {
        private string _id;        
        private string _itemId;
        //private string _storeId;
        //private string _branchId;
        private string _userFullName;
        private string _barcode;
        private string _name;
        private string _brand;
        private decimal _price;
        private int _quantity;
        private string _action;
        private DateTime _committedDate;
        private ObservableCollection<ItemHistoryModel> _itemHistoryRecords;


        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        //public string StoreId { get { return _storeId; } set { _storeId = value; OnPropertyChanged("StoreId"); } }
        //public string BranchId { get { return _branchId; } set { _branchId = value; OnPropertyChanged("BranchId"); } }
        public string UserFullName { get { return _userFullName; } set { _userFullName = value; OnPropertyChanged("UserFullName"); } }
        public string Barcode { get { return _barcode; } set { _barcode = value; OnPropertyChanged("Barcode"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Brand { get { return _brand; } set { _brand = value; OnPropertyChanged("Brand"); } }
        public decimal Price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public string Action { get { return _action; } set { _action = value; OnPropertyChanged("Action"); } }
        public DateTime CommittedDate { get { return _committedDate; } set { _committedDate = value; OnPropertyChanged("ComittedDate"); } }
        public ObservableCollection<ItemHistoryModel> ItemHistoryRecords { get { return _itemHistoryRecords; } set { _itemHistoryRecords = value; OnPropertyChanged("ItemHistoryRecords"); } }
        private void ItemHistoryRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ItemHistoryRecords");
        }
    }
}
