using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InventoryViewModel
    {
        private ItemRepository itemRepository;
        public ItemModel Item { get; set; }
        public ObservableCollection<ItemModel> ItemRecords { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand EditItemCommand { get; set; }


        public InventoryViewModel()
        {
            itemRepository = new ItemRepository();
            ItemRecords = itemRepository.GetAllItems();

            AddItemCommand = new RelayCommand(o => AddItem());

            EditItemCommand = new RelayCommand(o => EditItem());
        }

        private void AddItem()
        {
            InventoryAddView inventoryAddView = new InventoryAddView();
            inventoryAddView.Show();
        }

        private void EditItem()
        {
            InventoryEditView inventoryEditView = new InventoryEditView(Item);
            inventoryEditView.Show();
        }
    }
}
