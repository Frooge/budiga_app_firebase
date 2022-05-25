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
        public InventorySalesModel Sales { get; set; }
        public SalesInventoryViewModel(){
            //Sales = new SalesModel();
            //SalesRepository test = new SalesRepository();
            //Sales.OrderTransactionRecords = test.GetAllSales();
        }
        
    }

}