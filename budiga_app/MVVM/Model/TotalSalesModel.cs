using budiga_app.Core;

namespace budiga_app.MVVM.Model
{
    public class TotalSalesModel : ObservableObject
    {
        private int _transactions;
        private decimal _sales;

        public int Transactions { get { return _transactions; } set { _transactions = value; OnPropertyChanged("Transactions"); } }
        public decimal Sales { get { return _sales; } set { _sales = value; OnPropertyChanged("Sales"); } }
    }
}
