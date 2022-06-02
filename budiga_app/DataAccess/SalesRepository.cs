using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;

namespace budiga_app.DataAccess
{
    class SalesRepository
    {
        

        public SalesRepository()
        {
            
        }

        public ObservableCollection<InventorySalesModel> GetAllSales(string date)
        {
            ObservableCollection<InventorySalesModel> AllSales = new ObservableCollection<InventorySalesModel>();
            
            return AllSales;
        }


        public ObservableCollection<OverviewSalesModel> GetAllOverviewSales(string date)
        {
            ObservableCollection<OverviewSalesModel> overviewSales = new ObservableCollection<OverviewSalesModel>();
            
            return overviewSales;
        }

        public float GetTotalSales(string date)
        {
            
            float totalSales = 0;
            
            return totalSales;
        }

        public int GetTotalTransactions(string date)
        {           
            int totalTransactions = 0;
            
            return totalTransactions;
        }
    }
}
