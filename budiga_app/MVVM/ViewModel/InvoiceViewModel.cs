using budiga_app.Core;
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
    public class InvoiceViewModel
    {
        public RelayCommand AddQuantityCommand { get; set; }
        public RelayCommand ReduceQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public OrderModel Order { get; set; }

        public InvoiceViewModel()
        {
            Order = new OrderModel();
            Order.OrderTransactionRecords = new ObservableCollection<OrderModel>();
            AddQuantityCommand = new RelayCommand(param => AddQuantity((int)param));
            ReduceQuantityCommand = new RelayCommand(param => ReduceQuantity((int)param));
            RemoveItemCommand = new RelayCommand(param => RemoveItem((int)param));
            AddItemCommand = new RelayCommand(param => AddItem());
        }
        private void AddQuantity(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity < order.Item.Quantity)
            {
                order.Quantity += 1;
                CalculateSubtotal(order);
            }
        }

        private void ReduceQuantity(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity > 1)
            {
                order.Quantity -= 1;
                CalculateSubtotal(order);
            }
        }

        private void RemoveItem(int id)
        {
            OrderModel order = Order.OrderTransactionRecords.Where(i => i.ItemId == id).FirstOrDefault();
            Order.OrderTransactionRecords.Remove(order);
        }

        private void AddItem()
        {
            InvoiceAddView invoiceAddView = new InvoiceAddView(this);
            invoiceAddView.Show();
        }

        public bool GetItem(ItemModel item)
        {
            bool result = false;
            if (Order.OrderTransactionRecords.Where(i => i.ItemId == item.Id).FirstOrDefault() == null)
            {
                Order.OrderTransactionRecords.Add(new OrderModel()
                {
                    Id = 1,
                    ItemId = item.Id,
                    Quantity = 1,
                    SubtotalPrice = item.Price,
                    Item = item,
                });
                result = true;
            }            
            return result;
        }

        private void CalculateSubtotal(OrderModel order)
        {
            order.SubtotalPrice = order.Quantity * order.Item.Price;
        }
    }
}
