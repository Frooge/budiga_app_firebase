using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class ItemRepository
    {
        private dbConn database;
        private ItemModel _item;

        public ItemRepository()
        {
            database = new dbConn();
            _item = new ItemModel();
        }

        public ItemRepository(ItemModel item)
        {
            database = new dbConn();
            _item = item;
        }

        public ItemModel GetItem()
        {
            return _item;
        }

        public bool AddItem()
        {
            bool result = false;
            string query = string.Format("INSERT INTO item (name, brand, price, quantity) VALUES ('{0}', '{1}', '{2}', '{3}')", _item.Name, _item.Brand, _item.Price, _item.Quantity);
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
    }
}
