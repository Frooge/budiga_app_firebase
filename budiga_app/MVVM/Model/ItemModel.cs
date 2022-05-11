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
    public class ItemModel : ObservableObject
    {
        private int _id;
        private string _barcode;
        private string _name;
        private string _brand;
        private float _price;
        private int _quantity;
        private bool _isDeleted;
        private ObservableCollection<ItemModel> _itemRecords;


        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string Barcode { get { return _barcode; } set { _barcode = value; OnPropertyChanged("Barcode"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name");} }
        public string Brand { get { return _brand; } set { _brand = value; OnPropertyChanged("Brand"); } }
        public float Price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public bool IsDeleted { get { return _isDeleted; } set { _isDeleted = value; OnPropertyChanged("IsDeleted"); } }
        public ObservableCollection<ItemModel> ItemRecords { get { return _itemRecords; } set { _itemRecords = value; OnPropertyChanged("ItemRecords"); } }
        private void ItemRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ItemRecords");
        }

    }
}
