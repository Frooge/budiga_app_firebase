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
        private decimal _total;
        private string _storeId;
        private string _branchId;
        private string _storeName;
        private string _date;
        private ObservableCollection<OverviewSalesModel> _overviewSalesRecords;

            
        public int UnitsSold { get { return _unitsSold; } set { _unitsSold = value; OnPropertyChanged("UnitsSold"); } }
        public decimal Total { get { return _total; } set { _total = value; OnPropertyChanged("Total"); } }
        public string StoreId { get { return _storeId; } set { _storeId = value; OnPropertyChanged("StoreId"); } }
        public string BranchId { get { return _branchId; } set { _branchId = value; OnPropertyChanged("BranchId"); } }
        public string StoreName { get { return _storeName; } set { _storeName = value; OnPropertyChanged("StoreName"); } }
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public ObservableCollection<OverviewSalesModel> OverviewSalesRecords { get { return _overviewSalesRecords; } set { _overviewSalesRecords = value; OnPropertyChanged("OverviewSalesRecords"); } }
       
    }
}
