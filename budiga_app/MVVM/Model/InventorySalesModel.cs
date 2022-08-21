using budiga_app.Core;
using System.Collections.ObjectModel;

namespace budiga_app.MVVM.Model
{
    public class InventorySalesModel : ObservableObject
    {
        private int _unitsSold;
        private decimal _totalSales;
        private string _date;
        private string _storeName;
        private ItemModel _item;
        private ObservableCollection<InventorySalesModel> _inventorySalesRecords;

        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public decimal TotalSales { get { return _totalSales; } set { _totalSales = value; OnPropertyChanged("TotalSales"); } }
        public string StoreName { get { return _storeName; } set { _storeName = value; OnPropertyChanged("StoreName"); } }
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
        public ObservableCollection<InventorySalesModel> InventorySalesRecords { get { return _inventorySalesRecords; } set { _inventorySalesRecords = value; OnPropertyChanged("InventorySalesRecords"); } }
    }
}
