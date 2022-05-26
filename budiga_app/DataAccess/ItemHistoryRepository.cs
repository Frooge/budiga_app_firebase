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
    class ItemHistoryRepository
    {
        private dbConn database;

        public ItemHistoryRepository()
        {
            database = new dbConn();
        }

        public ObservableCollection<ItemHistoryModel> GetAllItemHistory()
        {
            ObservableCollection<ItemHistoryModel> ItemHistoryRecords = new ObservableCollection<ItemHistoryModel>();
            string query = "SELECT * FROM `item_history` ORDER BY `id` DESC";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    ItemHistoryRecords.Add(new ItemHistoryModel()
                    {
                        Id = reader.GetInt32("id"),
                        ItemId = reader.GetInt32("item_id"),
                        Barcode = reader.GetString("barcode"),
                        Name = reader.GetString("name"),
                        Brand = reader.GetString("brand"),
                        Price = reader.GetFloat("price"),
                        Quantity = reader.GetInt32("quantity"),
                        Action = reader.GetString("action"),
                        ComittedDate = reader.GetDateTime("comitted_date")
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
            return ItemHistoryRecords;
        }

        public bool AddItemHistory(ItemModel item, string action)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `item_history` (`item_id`, `barcode`, `name`, `brand`, `price`, `quantity`, `action`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", item.Id, item.Barcode, item.Name, item.Brand, item.Price, item.Quantity, action);
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

        public bool UndoAction(ItemHistoryModel itemHistory)
        {
            bool result = false;
            string query;
            switch (itemHistory.Action)
            {
                case "UPDATED":
                    query = string.Format("UPDATE `item` SET `name`='{0}', `barcode`='{1}', `brand`='{2}', `price`='{3}', `quantity`='{4}' WHERE `id`={5}", itemHistory.Name, itemHistory.Barcode, itemHistory.Brand, itemHistory.Price, itemHistory.Quantity, itemHistory.ItemId);
                    break;
                case "ADDED":
                    query = string.Format("UPDATE `item` SET `is_deleted`=`1` WHERE `id`={0}", itemHistory.ItemId);
                    break;
                case "DELETED":
                    query = string.Format("UPDATE `item` SET `is_deleted`=`0` WHERE `id`={0}", itemHistory.ItemId);
                    break;
                default:
                    query = "";
                    break;
            }
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully undid action", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
