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

        public ObservableCollection<ItemModel> GetAllItems()
        {
            ObservableCollection<ItemModel> items = new ObservableCollection<ItemModel>();
            string query ="SELECT * FROM item";
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
                        Name = reader.GetString("name"),
                        Brand = reader.GetString("brand"),
                        Price = reader.GetInt32("price"),
                        Quantity = reader.GetInt32("quantity")
                    });
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public bool AddItem(ItemModel item)
        {
            bool result = false;
            string query = string.Format("INSERT INTO item (name, brand, price, quantity) VALUES ('{0}', '{1}', '{2}', '{3}')", item.Name, item.Brand, item.Price, item.Quantity);
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
            return result;
        }

        public bool UpdateItem(ItemModel item)
        {
            bool result = false;
            string query = string.Format("UPDATE item SET name='{0}', brand='{1}', price='{2}',quantity='{3}' WHERE id={4}", item.Name, item.Brand, item.Price, item.Quantity, item.Id);
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
            return result;
        }
    }
}
