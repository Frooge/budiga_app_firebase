using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace budiga_app.MVVM.ViewModel
{


    public class SalesOverviewViewModel : ObservableObject
    {
        private SalesRepository salesRepository;
        private OverviewSalesModel _overviewSales;
        public OverviewSalesModel overviewSales { get; set; }

        public SalesOverviewViewModel()
        {
            salesRepository = new SalesRepository();
            _overviewSales = new OverviewSalesModel();
            overviewSales = new OverviewSalesModel();
            getAllOverviewItems();
        }

        private void getAllOverviewItems()
        {
            _overviewSales.overviewSales = salesRepository.GetAllOverviewSales();
            overviewSales.overviewSales = _overviewSales.overviewSales;
        }
    }

}