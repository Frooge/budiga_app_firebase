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
    class ItemRepository
    {
        private dbConn database;

        public ItemRepository()
        {
            database = new dbConn();
        }

        public ItemModel GetItem(int id)
        {
            ItemModel item = new ItemModel();
            string query = String.Format("SELECT * FROM `item` WHERE `id` = {0}", id);
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    item = new ItemModel()
                    {
                        Id = reader.GetInt32("id"),
                        Barcode = reader.GetString("barcode"),
                        Name = reader.GetString("name"),
                        Brand = reader.GetString("brand"),
                        Price = reader.GetFloat("price"),
                        Quantity = reader.GetInt32("quantity")
                    };
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
            return item;
        }

        public ObservableCollection<ItemModel> GetAllItems()
        {
            ObservableCollection<ItemModel> items = new ObservableCollection<ItemModel>();
            string query ="SELECT * FROM `item` WHERE `is_deleted` = 0";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new ItemModel()
                    {
                        Id = reader.GetInt32("id"),
                        Barcode = reader.GetString("barcode"),
                        Name = reader.GetString("name"),
                        Brand = reader.GetString("brand"),
                        Price = reader.GetFloat("price"),
                        Quantity = reader.GetInt32("quantity")
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
            return items;
        }

        public bool AddItem(ItemModel item)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `item` (`name`, `barcode`, `brand`, `price`, `quantity`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", item.Name, item.Barcode, item.Brand, item.Price, item.Quantity);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully added data", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public bool UpdateItem(ItemModel item)
        {
            bool result = false;
            string query = string.Format("UPDATE `item` SET `name`='{0}', `barcode`='{1}', `brand`='{2}', `price`='{3}', `quantity`='{4}' WHERE `id`={5}", item.Name, item.Barcode, item.Brand, item.Price, item.Quantity, item.Id);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully updated data", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public bool DeleteItem(int id)
        {
            bool result = false;
            string query = string.Format("UPDATE `item` SET `is_deleted`='1' WHERE `id`={0}", id);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully deleted data", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
