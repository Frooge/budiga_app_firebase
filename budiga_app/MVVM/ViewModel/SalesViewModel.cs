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
        

        //OverviewSales
        private OverviewSalesModel _overviewSales;
        public OverviewSalesModel overviewSales { get; set; }

        //Inventory Sales
        private InventorySalesModel _sales;
        public InventorySalesModel sales { get; set; }



        public RelayCommand getTotalSalesCommand { get; set; }  

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
            salesRepository = new SalesRepository();

            //Overview Sales Instantiate
            _overviewSales = new OverviewSalesModel();
            overviewSales = new OverviewSalesModel();

            //Inventory Sales Instantiate
            _sales = new InventorySalesModel();
            sales = new InventorySalesModel();
            try
            {
                SalesOverviewVM = new SalesOverviewViewModel();
                SalesInventoryVM = new SalesInventoryViewModel(sales);


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

            getTotalSalesCommand = new RelayCommand(param => GetTotals((string)param));
            GetTotals("Accumulated");
        }

        

        private void GetTotals(string date)
        {
            sales.InventorySales = salesRepository.GetAllSales(date);
        }

        private void getAllOverviewItems()
        {
            _overviewSales.overviewSales = salesRepository.GetAllOverviewSales();
            overviewSales.overviewSales = _overviewSales.overviewSales;
        }

    }
}
