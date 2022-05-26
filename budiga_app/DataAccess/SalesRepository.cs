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

        public ObservableCollection<InventorySalesModel> GetAllSales(string date)
        {
            string query;
            ObservableCollection<InventorySalesModel> AllSales = new ObservableCollection<InventorySalesModel>();
            switch (date)
            {
                case "Daily":
                    query = "SELECT `invoice`.created_date, `order`.item_id, `invoice`.total_price, `order`.quantity, DATE_FORMAT(created_date, \"%Y-%m-%d\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day;";
                    break;

                case "Monthly":
                    query = "SELECT `invoice`.created_date, `order`.item_id, `invoice`.total_price, `order`.quantity, DATE_FORMAT(created_date, \"%Y-%m\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY Created_day, `order`.item_id;";
                    break;

                case "Yearly":
                    query = "SELECT `invoice`.created_date, `order`.item_id, `invoice`.total_price, `order`.quantity, DATE_FORMAT(created_date, \"%Y\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day;";
                    break;

                default:
                    query = "SELECT `invoice`.created_date, `order`.item_id, `invoice`.total_price, `order`.quantity FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id;";
                    break;
            }
             
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
                        Date = reader.GetDateTime("created_date"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id")),
                        totalSales = reader.GetFloat("total_price"),
                        UnitsSold = reader.GetInt32("quantity")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return AllSales;
        }


        public ObservableCollection<OverviewSalesModel> GetAllOverviewSales()
        {
            ObservableCollection<OverviewSalesModel> overviewSales = new ObservableCollection<OverviewSalesModel>();
            string query = "SELECT created_date, SUM(total_price), COUNT(total_price) AS unit_sold, DATE_FORMAT(created_date,\"%Y-%m-%d\") AS Created_day FROM invoice GROUP BY Created_day;";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    overviewSales.Add(new OverviewSalesModel()
                    {
                        Total = reader.GetFloat("SUM(total_price)"),
                        Date = reader.GetDateTime("created_date"),
                        UnitsSold = reader.GetInt32("unit_sold")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return overviewSales;
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
