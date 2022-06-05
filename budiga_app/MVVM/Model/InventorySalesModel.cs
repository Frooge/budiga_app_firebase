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
        private string _date;
        private ItemModel _item;
        private ObservableCollection<InventorySalesModel> _inventorySalesRecords;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
        public ObservableCollection<InventorySalesModel> InventorySalesRecords { get { return _inventorySalesRecords; } set { _inventorySalesRecords = value; OnPropertyChanged("InventorySalesRecords"); } }
    }
}
