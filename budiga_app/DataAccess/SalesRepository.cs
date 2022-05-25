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
        private ItemRepository itemRepository;

        public SalesRepository()
        {
            database = new dbConn();
            itemRepository = new ItemRepository();
        }

        public ObservableCollection<InventorySalesModel> GetAllSales()
        {
            ObservableCollection<InventorySalesModel> AllSales = new ObservableCollection<InventorySalesModel>();
            string query = "SELECT SUM(total_price) FROM `invoice`";
            MySqlDataReader reader;

            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    AllSales.Add(new InventorySalesModel()
                    {
                        Id = reader.GetInt32("id"),
                        Date = reader.GetDateTime("date")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return AllSales;
        }

        public float getTotalSales()
        {
            float totalSales = 0;
            string query = "SELECT SUM(total_price) FROM `invoice`";
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                totalSales = (float)Convert.ToDecimal(commandDatabase.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return totalSales;
        }

        public int getTotalTransactions()
        {
            int totalTransactions = 0;
            string query = "SELECT COUNT(DISTINCT invoice_id) FROM `order`";
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                totalTransactions = Convert.ToInt32(commandDatabase.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return totalTransactions;
        }

        //public ObservableCollection<SalesModel> GetDailySales()
        //{
        //    ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
        //    string query = "SELECT * FROM `invoice` WHERE DAY(`created_date`) = DAY(now())";
        //    //string query = "SELECT * FROM `invoice` WHERE `created_date` >= now() + interval 1 day";
        //    MySqlDataReader reader;

        //    try
        //    {
        //        database.Connection();
        //        MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
        //        commandDatabase.CommandTimeout = 60;
        //        reader = commandDatabase.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            salesRecords.Add(new SalesModel()
        //            {
        //                Id = reader.GetInt32("id"),
        //                ItemId = reader.GetInt32("item_id"),
        //                InvoiceId = reader.GetInt32("invoice_id"),
        //                UnitsSold = reader.GetInt32("quantity"),
        //                SubtotalPrice = reader.GetFloat("subtotal_price"),
        //                Item = itemRepository.GetItem(reader.GetInt32("item_id"))
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return salesRecords;
        //}

        //public ObservableCollection<SalesModel> GetMonthlySales()
        //{
        //    ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
        //    string query = "SELECT * FROM `invoice` WHERE MONTH(`created_date`) = MONTH(now()) and YEAR(`created_date`) = YEAR(now())";
        //    //string query = "SELECT * FROM `invoice` WHERE `created_date` >= (LAST_DAY(NOW()) + interval 1 day - interval 1 month) AND `created_date` < (LAST_DAY(NOW()) + interval 1 day)";
        //    MySqlDataReader reader;

        //    try
        //    {
        //        database.Connection();
        //        MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
        //        commandDatabase.CommandTimeout = 60;
        //        reader = commandDatabase.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            salesRecords.Add(new SalesModel()
        //            {
        //                Id = reader.GetInt32("id"),
        //                ItemId = reader.GetInt32("item_id"),
        //                InvoiceId = reader.GetInt32("invoice_id"),
        //                UnitsSold = reader.GetInt32("quantity"),
        //                SubtotalPrice = reader.GetFloat("subtotal_price"),
        //                Item = itemRepository.GetItem(reader.GetInt32("item_id"))
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return salesRecords;
        //}

        //public ObservableCollection<SalesModel> GetYearlySales()
        //{
        //    ObservableCollection<SalesModel> salesRecords = new ObservableCollection<SalesModel>();
        //    string query = "SELECT * FROM `invoice` WHERE YEAR(`created_date`) = YEAR(now())";
            
        //    MySqlDataReader reader;

        //    try
        //    {
        //        database.Connection();
        //        MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
        //        commandDatabase.CommandTimeout = 60;
        //        reader = commandDatabase.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            salesRecords.Add(new SalesModel()
        //            {
        //                Id = reader.GetInt32("id"),
        //                ItemId = reader.GetInt32("item_id"),
        //                InvoiceId = reader.GetInt32("invoice_id"),
        //                UnitsSold = reader.GetInt32("quantity"),
        //                SubtotalPrice = reader.GetFloat("subtotal_price"),
        //                Item = itemRepository.GetItem(reader.GetInt32("item_id"))
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return salesRecords;
        //}

        //public float GetTotalSales()
        //{
        //    float totalSales = 0;

        //    string query = "SELECT * FROM `invoice`";
        //    MySqlDataReader reader;

        //    try
        //    {
        //        database.Connection();
        //        MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
        //        commandDatabase.CommandTimeout = 60;
        //        reader = commandDatabase.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            totalSales += reader.GetFloat("total_price");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    return totalSales;
        //}

        //public int GetTotalTransactions()
        //{
        //    int totalTransactions = 0;
        //    string query = "SELECT COUNT(*) FROM 'invoice'";

        //    try
        //    {
        //        database.Connection();
        //        MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
        //        commandDatabase.CommandTimeout = 60;
        //        totalTransactions = (int)commandDatabase.ExecuteScalar();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    return totalTransactions;
        //}

        // Sales Overview Table
        // SELECT invoice.created_date, SUM(`order`.`subtotal_price`) AS TOTAL, SUM(`order`.quantity) AS UNITS_SOLD FROM `order` INNER JOIN invoice ON `order`.invoice_id = invoice.id GROUP BY DAY(invoice.created_date)

        // Sales Inventory Table
        // SELECT `invoice.created_date`, `item.name`, `item.brand`, `item.price`, `order.quantity`, `order.subtotal_price` FROM ((`order` INNER JOIN `invoice` ON `invoice.id` = `order.invoice_id`)INNER JOIN `item.id` = `order.item_id`
        // unsure

        // Total Sales
        // SELECT SUM(`invoice.total_price`) FROM `invoice`

        // Transactions
        // SELECT COUNT(*) from `invoice`

        // Accumulated Sales
        // SELECT SUM(`invoice.total_price`) FROM `invoice`

        // Daily Sales
        // SELECT SUM(`total_price`) FROM `invoice` WHERE DAY(`created_date`) = DAY(now())

        // Monthly Sales
        // SELECT SUM(`total_price`) FROM `invoice` WHERE MONTH(`created_date`) = MONTH(now()) and YEAR(`created_date`) = YEAR(now())

        // Yearly Sales
        // SELECT SUM(`total_price`) FROM `invoice` WHERE YEAR(`created_date`) = YEAR(now())
    }
}
