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
        private UserRepository userRepository;

        public InvoiceRepository()
        {
            database = new dbConn();
            orderRepository = new OrderRepository();
            userRepository = new UserRepository();
        }

        public ObservableCollection<InvoiceModel> GetAllInvoice()
        {
            ObservableCollection<InvoiceModel> invoiceRecords = new ObservableCollection<InvoiceModel>();
            string query = "SELECT * FROM `invoice`";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    invoiceRecords.Add(new InvoiceModel()
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetInt32("user_id"),
                        TotalPrice = reader.GetFloat("total_price"),
                        CustomerPay = reader.GetFloat("customer_pay"),
                        CustomerChange = reader.GetFloat("customer_change"),
                        CreatedDate = reader.GetDateTime("created_date"),
                        InvoiceOrderRecords = orderRepository.GetInvoiceOrder(reader.GetInt32("id")),
                        User = userRepository.GetUserById(reader.GetInt32("user_id")),
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return invoiceRecords;
        }

        public InvoiceModel GetLastInvoice()
        {
            InvoiceModel invoice = new InvoiceModel();
            string query = "SELECT * FROM `invoice` ORDER BY `id` DESC LIMIT 1";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    invoice = new InvoiceModel()
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetInt32("user_id"),
                        TotalPrice = reader.GetFloat("total_price"),
                        CustomerPay = reader.GetFloat("customer_pay"),
                        CustomerChange = reader.GetFloat("customer_change"),
                        CreatedDate = reader.GetDateTime("created_date"),
                        InvoiceOrderRecords = orderRepository.GetInvoiceOrder(reader.GetInt32("id")),
                        User = userRepository.GetUserById(reader.GetInt32("user_id")),
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return invoice;
        }

        public bool AddInvoice(InvoiceModel invoice)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `invoice` (`user_id`, `total_price`, `customer_pay`, `customer_change`) VALUES ({0}, {1}, {2}, {3})", invoice.UserId, invoice.TotalPrice, invoice.CustomerPay, invoice.CustomerChange);
            string lastInsertId = ";SELECT LAST_INSERT_ID();";
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query + lastInsertId, database.conn);
                commandDatabase.CommandTimeout = 60;
                int newId = Convert.ToInt32(commandDatabase.ExecuteScalar());
                foreach(OrderModel order in invoice.InvoiceOrderRecords)
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
