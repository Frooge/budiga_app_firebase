using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class OverviewSalesModel : ObservableObject
    { 
            private int _unitsSold;
            private float _Total;
            private DateTime _date;
            private ItemModel _item;
            private ObservableCollection<OverviewSalesModel> _overviewSales;

            
            public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
            public float Total { get { return _Total; } set { _Total = value; OnPropertyChanged("Total"); } }
            public DateTime Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
            public ItemModel Item { get { return _item; } set { _item = value; OnPropertyChanged("Item"); } }
            public ObservableCollection<OverviewSalesModel> overviewSales { get { return _overviewSales; } set { _overviewSales = value; OnPropertyChanged("OverviewSales"); } }
       
    }
}
