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
        private dbConn database;
        private ItemRepository itemRepository;
        
        public OrderRepository()
        {
            database = new dbConn();
            itemRepository = new ItemRepository();
        }

        public ObservableCollection<OrderModel> GetInvoiceOrder(int invoiceId)
        {
            ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>();
            string query = String.Format("SELECT * FROM `order` WHERE `invoice_id` = {0}", invoiceId);
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        InvoiceId = reader.GetInt32("invoice_id"),
                        Quantity = reader.GetInt32("quantity"),
                        SubtotalPrice = reader.GetFloat("subtotal_price"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id"))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                database.Dispose();
            }
            return orders;
        }

        public bool AddOrder(int invoiceId, OrderModel order)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `order`(`item_id`, `invoice_id`, `quantity`, `subtotal_price`) VALUES ({0}, {1}, {2}, {3})", order.ItemId, invoiceId, order.Quantity, order.SubtotalPrice);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                database.Dispose();
            }
            return result;
        }
    }
}
