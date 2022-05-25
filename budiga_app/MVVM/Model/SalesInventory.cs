using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class SalesInventory : ObservableObject
    {
        private DateTime _date;
        private string _itemname;
        private string _brand;
        private float _price;
        private int _unitSold;
        private float _totalPrice;
        private ObservableCollection<SalesInventory> _salesInventory;

        public DateTime date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public string itemname { get { return _itemname; } set { _itemname = value; OnPropertyChanged("Item"); } }
        public string brand { get { return _brand; } set { _brand = value; OnPropertyChanged("Brand"); } }
        public float price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public int unitSold { get { return _unitSold; } set { _unitSold = value; OnPropertyChanged("UnitSold"); } }
        public float totalPrice { get { return _totalPrice; } set { _totalPrice = value; OnPropertyChanged("TotalPrice"); } }
        public ObservableCollection<SalesInventory> salesInventory { get { return _salesInventory; } set { _salesInventory = value; OnPropertyChanged("SalesInventory"); } }
    }
}
