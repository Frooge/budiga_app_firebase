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
        private int _id;
        private int _itemId;
        private string _barcode;
        private string _name;
        private string _brand;
        private float _price;
        private int _quantity;
        private string _action;
        private DateTime _comittedDate;
        private ObservableCollection<ItemHistoryModel> _itemHistoryRecords;


        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int ItemId { get { return _itemId; } set { _itemId = value; OnPropertyChanged("ItemId"); } }
        public string Barcode { get { return _barcode; } set { _barcode = value; OnPropertyChanged("Barcode"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Brand { get { return _brand; } set { _brand = value; OnPropertyChanged("Brand"); } }
        public float Price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public string Action { get { return _action; } set { _action = value; OnPropertyChanged("Action"); } }
        public DateTime ComittedDate { get { return _comittedDate; } set { _comittedDate = value; OnPropertyChanged("ComittedDate"); } }
        public ObservableCollection<ItemHistoryModel> ItemHistoryRecords { get { return _itemHistoryRecords; } set { _itemHistoryRecords = value; OnPropertyChanged("ItemHistoryRecords"); } }
        private void ItemHistoryRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ItemHistoryRecords");
        }
    }
}
