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

        public ItemRepository()
        {
            
        }

        public ItemModel GetItem(int id)
        {
            ItemModel item = new ItemModel();
            
            return item;
        }

        public ItemModel GetItemByBarcode(string barcode)
        {
            ItemModel item = new ItemModel();
            
            return item;
        }

        public ObservableCollection<ItemModel> GetAllItems()
        {
            ObservableCollection<ItemModel> items = new ObservableCollection<ItemModel>();
            
            return items;
        }

        public bool AddItem(ItemModel item)
        {
            bool result = false;
            
            return result;
        }

        public bool UpdateItem(ItemModel item)
        {
            bool result = false;
            
            return result;
        }

        public bool DeleteItem(int id)
        {
            bool result = false;
            
            return result;
        }
    }
}
