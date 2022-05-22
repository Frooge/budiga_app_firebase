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
        private dbConn database;
        private OrderRepository orderRepository;
        private UserRepository userRepository;
        private ItemRepository itemRepository;

        public SalesRepository()
        {
            database = new dbConn();
            orderRepository = new OrderRepository();
            userRepository = new UserRepository();
            itemRepository = new ItemRepository();
        }

        public ObservableCollection<SalesModel> GetAllSales()
        {
            ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
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
                    salesRecords.Add(new SalesModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        InvoiceId = reader.GetInt32("invoice_id"),
                        UnitsSold = reader.GetInt32("quantity"),
                        SubtotalPrice = reader.GetFloat("subtotal_price"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id"))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return salesRecords;
        }

        public ObservableCollection<SalesModel> GetDailySales()
        {
            ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
            string query = "SELECT * FROM `invoice` WHERE DAY(`created_date`) = DAY(now())";
            //string query = "SELECT * FROM `invoice` WHERE `created_date` >= now() + interval 1 day";
            MySqlDataReader reader;

            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    salesRecords.Add(new SalesModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        InvoiceId = reader.GetInt32("invoice_id"),
                        UnitsSold = reader.GetInt32("quantity"),
                        SubtotalPrice = reader.GetFloat("subtotal_price"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id"))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return salesRecords;
        }

        public ObservableCollection<SalesModel> GetMonthlySales()
        {
            ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
            string query = "SELECT * FROM `invoice` WHERE MONTH(`created_date`) = MONTH(now()) and YEAR(`created_date`) = YEAR(now())";
            //string query = "SELECT * FROM `invoice` WHERE `created_date` >= (LAST_DAY(NOW()) + interval 1 day - interval 1 month) AND `created_date` < (LAST_DAY(NOW()) + interval 1 day)";
            MySqlDataReader reader;

            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    salesRecords.Add(new SalesModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        InvoiceId = reader.GetInt32("invoice_id"),
                        UnitsSold = reader.GetInt32("quantity"),
                        SubtotalPrice = reader.GetFloat("subtotal_price"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id"))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return salesRecords;
        }

        public ObservableCollection<SalesModel> GetYearlySales()
        {
            ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
            string query = "SELECT * FROM `invoice` WHERE YEAR(`created_date`) = YEAR(now())";
            
            MySqlDataReader reader;

            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    salesRecords.Add(new SalesModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        InvoiceId = reader.GetInt32("invoice_id"),
                        UnitsSold = reader.GetInt32("quantity"),
                        SubtotalPrice = reader.GetFloat("subtotal_price"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id"))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return salesRecords;
        }

        public float GetTotalSales()
        {
            float totalSales = 0;

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
                    totalSales += reader.GetFloat("total_price");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return totalSales;
        }

        public int GetTotalTransactions()
        {
            int totalTransactions = 0;
            string query = "SELECT COUNT(*) FROM 'invoice'";

            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                totalTransactions = (int)commandDatabase.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return totalTransactions;
        }

    }
}
