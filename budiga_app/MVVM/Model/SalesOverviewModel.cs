using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class SalesOverviewModel : ObservableObject
    {
        private DateTime _date;
        private int _unitsSold;
        private float _totalUnitsSold;
        private ObservableCollection<SalesOverviewModel> _salesOverviewModel;

        public DateTime date { get {return _date;} set { _date = value; OnPropertyChanged("Date"); } }
        public int _unitSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("unitSold"); } }
        public float totalUnitsSold { get { return _totalUnitsSold; } set { _totalUnitsSold = value; OnPropertyChanged("TotalUnitsSold"); } }
        public ObservableCollection<SalesOverviewModel> salesOverviewModel { get { return _salesOverviewModel; } set { _salesOverviewModel = value; OnPropertyChanged("SalesOverviewModel"); } }
    }
}
