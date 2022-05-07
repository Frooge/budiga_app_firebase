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
        int _id;
        string _name;
        string _brand;
        int _price;
        int _quantity;
        ObservableCollection<ItemModel> _itemRecords;


        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name");} }
        public string Brand { get { return _brand; } set { _brand = value; OnPropertyChanged("Brand"); } }
        public int Price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public ObservableCollection<ItemModel> ItemRecords { get { return _itemRecords; } set { _itemRecords = value; OnPropertyChanged("ItemRecords"); } }
        private void ItemModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ItemModel");
        }
    }
}
