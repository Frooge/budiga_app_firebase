using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class OrderRepository
    {
        
        public OrderRepository()
        {
            
        }

        public ObservableCollection<OrderModel> GetInvoiceOrder(int invoiceId)
        {
            ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>();
            
            return orders;
        }

        public bool AddOrder(int invoiceId, OrderModel order)
        {
            bool result = false;
            
            return result;
        }
    }
}
