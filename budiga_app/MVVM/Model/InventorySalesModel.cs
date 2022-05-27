using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class InventorySalesModel : ObservableObject
    {
        private int _id;
        private int _unitsSold;
        private float _totalSales;
        private int _totalTransactions;
        private string _date;
        private ItemModel _item;
        private ObservableCollection<InventorySalesModel> _inventorySales;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public float TotalSales { get { return _totalSales; } set { _totalSales = value; OnPropertyChanged("TotalSales"); } }
        public int TotalTransaction { get { return _totalTransactions; } set { _totalTransactions = value; OnPropertyChanged("TotalTransactions"); } }
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
        public ObservableCollection<InventorySalesModel> InventorySales { get { return _inventorySales; } set { _inventorySales = value; OnPropertyChanged("InventorySales"); } }
    }
}
