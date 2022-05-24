using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InventoryHistoryViewModel
    {
        private ItemHistoryRepository itemHistoryRepository;
        public ItemHistoryModel ItemHistory { get; set; }
        public RelayCommand UndoActionCommand { get; set; }

        public InventoryHistoryViewModel()
        {
            itemHistoryRepository = new ItemHistoryRepository();
            ItemHistory = new ItemHistoryModel();
            UndoActionCommand = new RelayCommand(param => UndoAction((ItemHistoryModel) param));
            GetAll();
        }

        public void GetAll()
        {
            ItemHistory.ItemHistoryRecords = itemHistoryRepository.GetAllItemHistory();
        }

        private void UndoAction(ItemHistoryModel item)
        {
            itemHistoryRepository.UndoAction(item);
        }
    }
}
