using budiga_app.Core;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    class InvoiceViewModel
    {
        public RelayCommand AddQuantityCommand { get; set; }
        public RelayCommand ReduceQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public ItemModel ItemModel { get; set; }

        public InvoiceViewModel()
        {
            ItemModel = new ItemModel();
            AddQuantityCommand = new RelayCommand(param => AddQuantity((int)param));
            ReduceQuantityCommand = new RelayCommand(param => ReduceQuantity((int)param));
            RemoveItemCommand = new RelayCommand(param => RemoveItem((int)param));
            GetInvoice();
        }

        private void GetInvoice()
        {
            ItemModel.ItemRecords = new ObservableCollection<ItemModel>()
            {
                new ItemModel()
                {
                    Id = 1,
                    Name = "Hi",
                    Barcode = "xxx",
                    Brand = "Hello",
                    Price = 10,
                    Quantity = 2
                },
                new ItemModel()
                {
                    Id = 2,
                    Name = "Yow",
                    Barcode = "xxx",
                    Brand = "Ayo",
                    Price = 20,
                    Quantity = 4
                }
            };
        }

        private void AddQuantity(int id)
        {
            ItemModel item = ItemModel.ItemRecords.Where(i => i.Id == id).FirstOrDefault();
            item.Quantity += 1;
        }

        private void ReduceQuantity(int id)
        {
            ItemModel item = ItemModel.ItemRecords.Where(i => i.Id == id).FirstOrDefault();
            if(item.Quantity > 0)
            {
                item.Quantity -= 1;
            }
        }

        private void RemoveItem(int id)
        {
            ItemModel item = ItemModel.ItemRecords.Where(i => i.Id == id).FirstOrDefault();
            ItemModel.ItemRecords.Remove(item);
        }
    }
}
