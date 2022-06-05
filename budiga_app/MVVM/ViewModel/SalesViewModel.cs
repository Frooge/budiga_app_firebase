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
        

        
        private OverviewSalesModel _overviewSales;
        public OverviewSalesModel OverviewSales { get; set; }

        private InventorySalesModel _sales;
        public InventorySalesModel Sales { get; set; }

        public RelayCommand GetAllCommand { get; set; }
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public SalesOverviewViewModel SalesOverviewVM { get; set; }
        public SalesInventoryViewModel SalesInventoryVM { get; set; }

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

        private static SalesViewModel salesVM;

        public static SalesViewModel GetInstance
        {
            get
            {
                if (salesVM == null)
                {
                    salesVM = new SalesViewModel();
                }
                return salesVM;
            }
        }


        public SalesViewModel()
        {
            salesRepository = new SalesRepository();

            //Overview Sales Instantiate
            _overviewSales = new OverviewSalesModel();
            OverviewSales = new OverviewSalesModel();

            //Inventory Sales Instantiate
            _sales = new InventorySalesModel();
            Sales = new InventorySalesModel();
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

            GetAllCommand = new RelayCommand(param => GetAll((string)param));
            GetAll("Accumulated");
        }        

        private void GetAll(string date)
        {
            Sales.TotalSales = salesRepository.GetTotalSales(date);
            Sales.TotalTransaction = salesRepository.GetTotalTransactions(date);

            _sales.SalesRecords = salesRepository.GetAllSales(date);
            Sales.SalesRecords = _sales.SalesRecords;            

            _overviewSales.OverviewSalesRecords = salesRepository.GetAllOverviewSales(date);
            OverviewSales.OverviewSalesRecords = _overviewSales.OverviewSalesRecords;
        }

    }
}
