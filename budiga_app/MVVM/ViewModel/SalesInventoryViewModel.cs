using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace budiga_app.MVVM.ViewModel
{
    public class SalesInventoryViewModel
    {
        SalesRepository salesRepository;
        private InventorySalesModel _sales;
        public InventorySalesModel sales { get; set; }
        public SalesInventoryViewModel()
        {
            salesRepository = new SalesRepository();
            _sales = new InventorySalesModel();
            sales = new InventorySalesModel();
            getAllSales();
        }

        public void getAllSales()
        {
            _sales.InventorySales = salesRepository.GetAllSales();
            sales.InventorySales = _sales.InventorySales;
        }
        
    }

}