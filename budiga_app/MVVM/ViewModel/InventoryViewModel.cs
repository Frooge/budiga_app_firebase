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
    public class InventoryViewModel : ObservableObject
    {
        private ItemRepository itemRepository;
        public ItemModel ItemModel { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand EditItemCommand { get; set; }

        public InventoryViewModel()
        {
            itemRepository = new ItemRepository();
            ItemModel = new ItemModel();
            AddItemCommand = new RelayCommand(o => AddItem());
            EditItemCommand = new RelayCommand(o => EditItem((ItemModel)o));
            GetAll();
        }

        public void GetAll()
        {
            ItemModel.ItemRecords = itemRepository.GetAllItems();
        }

        private void AddItem()
        {
            InventoryAddView inventoryAddView = new InventoryAddView(this);
            inventoryAddView.Show();
        }

        private void EditItem(ItemModel item)
        {
            InventoryEditView inventoryEditView = new InventoryEditView(this, item);
            inventoryEditView.Show();
        }
    }
}
