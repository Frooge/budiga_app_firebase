﻿using budiga_app.Core;
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

        public InventoryViewModel()
        {
            itemRepository = new ItemRepository();
            Item = new ItemModel();
            AddItemCommand = new RelayCommand(param => AddItem());
            EditItemCommand = new RelayCommand(param => EditItem((ItemModel)param));
            GetAll();
        }

        public void GetAll()
        {
            Item.ItemRecords = itemRepository.GetAllItems();
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
