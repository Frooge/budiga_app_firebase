using budiga_app.Core;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceViewModel
    {
        public RelayCommand AddQuantityCommand { get; set; }
        public RelayCommand ReduceQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand CancelOrderCommand { get; set; }
        public RelayCommand CheckoutCommand { get; set; }
        public InvoiceModel Invoice { get; set; }

        public InvoiceViewModel()
        {
            Invoice = new InvoiceModel();
            Invoice.InvoiceOrderRecords = new ObservableCollection<OrderModel>();
            AddQuantityCommand = new RelayCommand(param => AddQuantity((int)param));
            ReduceQuantityCommand = new RelayCommand(param => ReduceQuantity((int)param));
            RemoveItemCommand = new RelayCommand(param => RemoveItem((int)param));
            CancelOrderCommand = new RelayCommand(param => CancelOrder());
            CheckoutCommand = new RelayCommand(param => Checkout());
            AddItemCommand = new RelayCommand(param => AddItem());
        }
        private void AddQuantity(int id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity < order.Item.Quantity)
            {
                order.Quantity += 1;
                CalculateSubtotal(order);
                CalculateTotal();
            }
        }

        private void ReduceQuantity(int id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity > 1)
            {
                order.Quantity -= 1;
                CalculateSubtotal(order);
                CalculateTotal();
            }
        }

        private void RemoveItem(int id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            Invoice.InvoiceOrderRecords.Remove(order);
            CalculateTotal();
        }

        private void AddItem()
        {
            if(Invoice.InvoiceOrderRecords.Count > 0)
            {
                InvoiceAddView invoiceAddView = new InvoiceAddView(this);
                invoiceAddView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invoice list is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelOrder()
        {
            Invoice.InvoiceOrderRecords.Clear();
            CalculateTotal();
        }

        private void Checkout()
        {
            CalculateTotal();
            InvoicePayView invoicePayView = new InvoicePayView(Invoice);
            invoicePayView.ShowDialog();
        }

        public bool GetItem(ItemModel item)
        {
            bool result = false;
            if (Invoice.InvoiceOrderRecords.Where(i => i.ItemId == item.Id).FirstOrDefault() == null)
            {
                Invoice.InvoiceOrderRecords.Add(new OrderModel()
                {
                    Id = 1,
                    ItemId = item.Id,
                    Quantity = 1,
                    SubtotalPrice = item.Price,
                    Item = item,
                });
                CalculateTotal();
                result = true;
            }            
            return result;
        }

        private void CalculateSubtotal(OrderModel order)
        {
            order.SubtotalPrice = order.Quantity * order.Item.Price;
        }

        private void CalculateTotal()
        {
            Invoice.TotalPrice = 0;
            foreach(OrderModel order in Invoice.InvoiceOrderRecords)
            {
                Invoice.TotalPrice += order.SubtotalPrice;
            }
        }
    }
}
