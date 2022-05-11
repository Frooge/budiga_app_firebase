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
    class InvoiceRepository
    {
        private dbConn database;
        private OrderRepository orderRepository;

        public InvoiceRepository()
        {
            database = new dbConn();
            orderRepository = new OrderRepository(database);
        }

        public bool AddInvoice(InvoiceModel invoice, ObservableCollection<OrderModel> invoiceOrderRecords)
        {
            bool result = false;
            string query = string.Format("INSERT INTO invoice (user_id, total_price, customer_pay, customer_change) VALUES ('{0}', '{1}', '{2}', '{3}')", invoice.UserId, invoice.TotalPrice, invoice.CustomerPay, invoice.CustomerPay);
            string lastInsertId = ";SELECT LAST_INSERT_ID();";
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query + lastInsertId, database.conn);
                commandDatabase.CommandTimeout = 60;
                int newId = (int)commandDatabase.ExecuteScalar();
                foreach(OrderModel order in invoiceOrderRecords)
                {
                    orderRepository.AddOrder(newId, order);
                }
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }
    }
}
