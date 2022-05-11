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
        public OrderModel Order { get; set; }

        public InvoiceViewModel()
        {
            Order = new OrderModel();
            AddQuantityCommand = new RelayCommand(param => AddQuantity((int)param));
            ReduceQuantityCommand = new RelayCommand(param => ReduceQuantity((int)param));
            RemoveItemCommand = new RelayCommand(param => RemoveItem((int)param));
            GetInvoice();
        }

        private void GetInvoice()
        {
            Order.OrderTransactionRecords = new ObservableCollection<OrderModel>()
            {
                new OrderModel()
                {
                    Id = 1,
                    Quantity = 1,
                    SubtotalPrice = 0,
                    Item = new ItemModel()
                    {
                        Id = 1,
                        Name = "Hi",
                        Barcode = "xxx",
                        Brand = "Hello",
                        Price = 10,
                        Quantity = 2
                    }                    
                },
                new OrderModel()
                {
                    Id = 2,
                    Quantity = 2,
                    SubtotalPrice = 0,
                    Item = new ItemModel()
                    {
                        Id = 2,
                        Name = "Yow",
                        Barcode = "xxx",
                        Brand = "Ayo",
                        Price = 20,
                        Quantity = 4
                    }
                }
            };
        }

        private void AddQuantity(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.Id== id).FirstOrDefault();
            if(order.Quantity < order.Item.Quantity)
            {
                order.Quantity += 1;
            }
        }

        private void ReduceQuantity(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.Id == id).FirstOrDefault(); ;
            if(order.Quantity > 0)
            {
                order.Quantity -= 1;
            }
        }

        private void RemoveItem(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.Id == id).FirstOrDefault();
            Order.OrderTransactionRecords.Remove(order);
        }
    }
}
