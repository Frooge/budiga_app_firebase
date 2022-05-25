using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using budiga_app.MVVM.Model;
using budiga_app.DataAccess;

namespace budiga_app.MVVM.ViewModel
{
    public class SalesViewModel : ObservableObject
    {
        private SalesRepository salesRepository;
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public SalesOverviewViewModel SalesOverviewVM { get; set; }
        public SalesInventoryViewModel SalesInventoryVM { get; set; }
        public InventorySalesModel Sales { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
            
        }

        public SalesViewModel()
        {
            try
            {
                SalesOverviewVM = new SalesOverviewViewModel();
                SalesInventoryVM = new SalesInventoryViewModel();

                CurrentView = SalesOverviewVM;

                OverviewViewCommand = new RelayCommand(o =>
                {
                    CurrentView = SalesOverviewVM;
                });

                InventoryViewCommand = new RelayCommand(o =>
                {
                    CurrentView = SalesInventoryVM;
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

            Sales = new InventorySalesModel();
            salesRepository = new SalesRepository();
            GetTotals();
        }

        private void GetTotals()
        {
            Sales.totalSales = salesRepository.getTotalSales();
            Sales.totalTransaction = salesRepository.getTotalTransactions();
        }
       
    }
}
