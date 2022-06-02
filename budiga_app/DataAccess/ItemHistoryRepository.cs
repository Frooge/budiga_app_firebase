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

        public ItemHistoryRepository()
        {

        }

        public ObservableCollection<ItemHistoryModel> GetAllItemHistory()
        {
            ObservableCollection<ItemHistoryModel> ItemHistoryRecords = new ObservableCollection<ItemHistoryModel>();
            
            return ItemHistoryRecords;
        }

        public bool AddItemHistory(ItemModel item, string action)
        {
            bool result = false;
            
            return result;
        }

        public bool UndoAction(ItemHistoryModel itemHistory)
        {
            bool result = false;
            
            return result;
        }
    }
}
