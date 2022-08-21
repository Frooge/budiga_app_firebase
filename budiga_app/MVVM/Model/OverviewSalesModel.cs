using budiga_app.Core;
using System.Collections.ObjectModel;

namespace budiga_app.MVVM.Model
{
    public class OverviewSalesModel : ObservableObject
    {
        private int _unitsSold;
        private decimal _total;
        private string _storeName;
        private string _date;
        private ObservableCollection<OverviewSalesModel> _overviewSalesRecords;


        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public decimal Total { get { return _total; } set { _total = value; OnPropertyChanged("Total"); } }
        public string StoreName { get { return _storeName; } set { _storeName = value; OnPropertyChanged("StoreName"); } }
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public ObservableCollection<OverviewSalesModel> OverviewSalesRecords { get { return _overviewSalesRecords; } set { _overviewSalesRecords = value; OnPropertyChanged("OverviewSalesRecords"); } }

    }
}
