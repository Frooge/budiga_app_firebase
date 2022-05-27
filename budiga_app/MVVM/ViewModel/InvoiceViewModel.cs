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
using System.Windows;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceViewModel
    {
        public RelayCommand AddQuantityCommand { get; set; }
        public RelayCommand ReduceQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand TransactionHistoryCommand { get; set; }
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
            AddItemCommand = new RelayCommand(param => AddItem());
            TransactionHistoryCommand = new RelayCommand(param => TransactionHistory());
            CancelOrderCommand = new RelayCommand(param => CancelOrder());
            CheckoutCommand = new RelayCommand(param => Checkout());            
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
            InvoiceAddView invoiceAddView = new InvoiceAddView(this);
            invoiceAddView.ShowDialog();            
        }

        private void TransactionHistory()
        {
            InvoiceHistoryView invoiceHistoryView = new InvoiceHistoryView();
            invoiceHistoryView.ShowDialog();
        }

        public void CancelOrder()
        {
            Invoice.InvoiceOrderRecords.Clear();
            CalculateTotal();
        }

        private void Checkout()
        {
            if (Invoice.InvoiceOrderRecords.Count > 0)
            {
                CalculateTotal();
                InvoicePayView invoicePayView = new InvoicePayView(this, Invoice);
                invoicePayView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invoice list is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
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

        public bool GetItemByBarcode(string barcode)
        {
            bool result = false;
            ItemModel item = new ItemModel();
            ItemRepository itemRepository = new ItemRepository();
            item = itemRepository.GetItemByBarcode(barcode);
            if (item.Id != -1 &&  Invoice.InvoiceOrderRecords.Where(i => i.ItemId == item.Id).FirstOrDefault() == null)
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
