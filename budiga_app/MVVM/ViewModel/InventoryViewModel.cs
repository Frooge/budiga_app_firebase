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
        public ItemModel Item { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand EditItemCommand { get; set; }
        public RelayCommand SearchItemCommand { get; set; }
        private ItemModel _item;

        public InventoryViewModel()
        {
            itemRepository = new ItemRepository();
            _item = new ItemModel();
            Item = new ItemModel();
            AddItemCommand = new RelayCommand(param => AddItem());
            EditItemCommand = new RelayCommand(param => EditItem((ItemModel)param));
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
            GetAll();
        }

        public void GetAll()
        {
            _item.ItemRecords = itemRepository.GetAllItems();
            Item.ItemRecords = _item.ItemRecords;
        }

        private void SearchItem(string searchTxt = "")
        {
            Item.ItemRecords = new ObservableCollection<ItemModel>(
                _item.ItemRecords.Where(i => i.Name.ToLower().Contains(searchTxt.ToLower())
                || i.Brand.ToLower().Contains(searchTxt.ToLower())
                || i.Barcode.ToLower().Contains(searchTxt.ToLower())).ToList());               
        }

        private void AddItem()
        {
            InventoryAddView inventoryAddView = new InventoryAddView(this);
            inventoryAddView.ShowDialog();
        }

        private void EditItem(ItemModel item)
        {
            InventoryEditView inventoryEditView = new InventoryEditView(this, item);
            inventoryEditView.ShowDialog();
        }
    }
}
