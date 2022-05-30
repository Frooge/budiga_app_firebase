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
                    query = "SELECT `invoice`.created_date, `order`.item_id, SUM(`order`.quantity) AS quantity, SUM(`order`.quantity) * `item`.price AS total_price, DATE_FORMAT(created_date, \"%Y-%m-%d\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day ORDER BY Created_day DESC;";
                    break;

                case "Monthly":
                    query = "SELECT `invoice`.created_date, `order`.item_id, SUM(`order`.quantity) AS quantity, SUM(`order`.quantity) * `item`.price AS total_price, DATE_FORMAT(created_date, \"%Y-%m\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day ORDER BY Created_day DESC;";
                    break;

                case "Yearly":
                    query = "SELECT `invoice`.created_date, `order`.item_id, SUM(`order`.quantity) AS quantity, SUM(`order`.quantity) * `item`.price AS total_price, DATE_FORMAT(created_date, \"%Y\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day ORDER BY Created_day DESC;";
                    break;

                default:
                    query = "SELECT `invoice`.created_date, `order`.item_id, SUM(`order`.quantity) AS quantity, SUM(`order`.quantity) * `item`.price AS total_price, DATE_FORMAT(created_date, \"%Y-%m-%d\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id INNER JOIN item ON `order`.item_id = `item`.id GROUP BY `order`.item_id, Created_day ORDER BY Created_day DESC;";
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
                        Date = reader.GetString("Created_day"),
                        Item = itemRepository.GetItem(reader.GetInt32("item_id")),
                        TotalSales = reader.GetFloat("total_price"),
                        UnitsSold = reader.GetInt32("quantity")
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
            return AllSales;
        }


        public ObservableCollection<OverviewSalesModel> GetAllOverviewSales(string date)
        {
            string query;
            ObservableCollection<OverviewSalesModel> overviewSales = new ObservableCollection<OverviewSalesModel>();
            switch (date)
            {
                case "Daily":
                    query = "SELECT `invoice`.created_date, SUM(`invoice`.total_price) AS total_price, SUM(`order`.quantity) AS unit_sold, DATE_FORMAT(created_date,\"%Y-%m-%d\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id GROUP BY Created_day ORDER BY Created_day DESC;";
                    break;

                case "Monthly":
                    query = "SELECT `invoice`.created_date, SUM(`invoice`.total_price) AS total_price, SUM(`order`.quantity) AS unit_sold, DATE_FORMAT(created_date,\"%Y-%m\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id GROUP BY Created_day ORDER BY Created_day DESC;";
                    break;

                case "Yearly":
                    query = "SELECT `invoice`.created_date, SUM(`invoice`.total_price) AS total_price, SUM(`order`.quantity) AS unit_sold, DATE_FORMAT(created_date,\"%Y\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id GROUP BY Created_day ORDER BY Created_day DESC;";
                    break;

                default:
                    query = "SELECT `invoice`.created_date, SUM(`invoice`.total_price) AS total_price, SUM(`order`.quantity) AS unit_sold, DATE_FORMAT(created_date,\"%Y-%m-%d\") AS Created_day FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id GROUP BY Created_day ORDER BY Created_day DESC;";
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
                    overviewSales.Add(new OverviewSalesModel()
                    {
                        Total = reader.GetFloat("total_price"),
                        Date = reader.GetString("Created_day"),
                        UnitsSold = reader.GetInt32("unit_sold")
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
            return overviewSales;
        }

        public float GetTotalSales(string date)
        {
            string query;
            object result;
            float totalSales = 0;
            switch (date)
            {
                case "Daily":
                    query = "SELECT SUM(`invoice`.total_price) AS total_price FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id WHERE DAY(CURRENT_DATE) = DAY(`invoice`.created_date);";
                    break;

                case "Monthly":
                    query = "SELECT SUM(`invoice`.total_price) AS total_price FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id WHERE MONTH(CURRENT_DATE) = MONTH(`invoice`.created_date);";
                    break;

                case "Yearly":
                    query = "SELECT SUM(`invoice`.total_price) AS total_price FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id WHERE YEAR(CURRENT_DATE) = YEAR(`invoice`.created_date);";
                    break;

                default:
                    query = "SELECT SUM(`invoice`.total_price) AS total_price FROM `invoice` INNER JOIN `order` ON `invoice`.id = `order`.invoice_id;";
                    break;
            }
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                result = commandDatabase.ExecuteScalar();
                if (!result.Equals(DBNull.Value))
                {
                    totalSales = float.Parse(Convert.ToString(result));
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
            return totalSales;
        }

        public int GetTotalTransactions(string date)
        {
            string query;
            object result;
            int totalTransactions = 0;
            switch (date)
            {
                case "Daily":
                    query = "SELECT COUNT(id) FROM `invoice` WHERE DAY(CURRENT_DATE) = DAY(created_date);";
                    break;

                case "Monthly":
                    query = "SELECT COUNT(id) FROM `invoice` WHERE MONTH(CURRENT_DATE) = MONTH(created_date);";
                    break;

                case "Yearly":
                    query = "SELECT COUNT(id) FROM `invoice` WHERE YEAR(CURRENT_DATE) = YEAR(created_date);";
                    break;

                default:
                    query = "SELECT COUNT(id) FROM `invoice`;";
                    break;
            }
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                result = commandDatabase.ExecuteScalar();
                if (!result.Equals(DBNull.Value))
                {
                    totalTransactions = int.Parse(Convert.ToString(result));
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
            return totalTransactions;
        }
    }
}
