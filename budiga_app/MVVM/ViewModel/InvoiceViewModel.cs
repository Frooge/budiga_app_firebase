using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceViewModel
    {
        private static InvoiceViewModel _instance;
        public Action ClosePayView { get; set; }
        public RelayCommand AddQuantityCommand { get; set; }
        public RelayCommand ReduceQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand TransactionHistoryCommand { get; set; }
        public RelayCommand CancelOrderCommand { get; set; }
        public RelayCommand CheckoutCommand { get; set; }
        public RelayCommand ConfirmPayCommand { get; set; }
        public RelayCommand GetReceiptCommand { get; set; }
        public RelayCommand SearchItemCommand { get; set; }

        private InvoiceModel _invoice { get; set; }
        public InvoiceModel Invoice { get; set; }
        public OrderModel Order { get; set; }
        public PageModel Page { get; set; }

        public static InvoiceViewModel GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InvoiceViewModel();
                }
                return _instance;
            }
        }
        public static void ReleaseInstance()
        {
            _instance = null;
        }
        public InvoiceViewModel()
        {
            Initialize();
            GetAllInvoice();
        }

        private void Initialize()
        {
            _invoice = new InvoiceModel();
            Invoice = new InvoiceModel();
            Order = new OrderModel();
            Page = new PageModel();
            Invoice.InvoiceOrderRecords = new ObservableCollection<OrderModel>();
            AddQuantityCommand = new RelayCommand(param => AddQuantity((string)param));
            ReduceQuantityCommand = new RelayCommand(param => ReduceQuantity((string)param));
            RemoveItemCommand = new RelayCommand(param => RemoveItem((string)param));
            AddItemCommand = new RelayCommand(param => AddItem());
            TransactionHistoryCommand = new RelayCommand(param => TransactionHistory());
            CancelOrderCommand = new RelayCommand(param => CancelOrder());
            CheckoutCommand = new RelayCommand(param => Checkout());
            ConfirmPayCommand = new RelayCommand(param => ConfirmPay(Convert.ToDecimal(param)));
            GetReceiptCommand = new RelayCommand(param => GetReceipt((InvoiceModel)param));
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
        }

        private void GetAllInvoice()
        {
            try
            {
                DataClass dataClass = DataClass.GetInstance;
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Invoice");

                FirestoreChangeListener listener = query.Listen(async snapshot =>
                {
                    Page.IsLoading = true;
                    _invoice.InvoiceRecords = new ObservableCollection<InvoiceModel>();
                    Order.OrderRecords = new ObservableCollection<OrderModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        Query ordersQuery = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Orders").WhereEqualTo("InvoiceId", dict["Id"]);
                        QuerySnapshot ordersQuerySnapshot = await ordersQuery.GetSnapshotAsync();
                        foreach (DocumentSnapshot orderDocumentSnapshot in ordersQuerySnapshot.Documents)
                        {
                            Dictionary<string, object> orderDict = orderDocumentSnapshot.ToDictionary();
                            DocumentReference itemRef = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Items").Document(orderDict["ItemId"].ToString());
                            DocumentSnapshot snapshotItem = await itemRef.GetSnapshotAsync();
                            Dictionary<string, object> itemDict = snapshotItem.ToDictionary();
                            Order.OrderRecords.Add(new OrderModel
                            {
                                Id = orderDict["Id"].ToString(),
                                ItemId = orderDict["ItemId"].ToString(),
                                InvoiceId = orderDict["InvoiceId"].ToString(),
                                Quantity = Convert.ToInt32(orderDict["Quantity"]),
                                ActualItemPrice = Convert.ToDecimal(orderDict["ActualItemPrice"]),
                                SubtotalPrice = Convert.ToDecimal(orderDict["SubtotalPrice"]),
                                Item = new ItemModel
                                {
                                    Id = itemDict["Id"].ToString(),
                                    Barcode = itemDict["Barcode"].ToString(),
                                    Name = itemDict["Name"].ToString(),
                                    Brand = itemDict["Brand"].ToString(),
                                    Price = Convert.ToDecimal(itemDict["Price"]),
                                    Quantity = Convert.ToInt32(itemDict["Quantity"])
                                }
                            });
                        }

                        _invoice.InvoiceRecords.Add(new InvoiceModel()
                        {
                            Id = dict["Id"].ToString(),
                            UserFullName = dict["UserFullName"].ToString(),
                            BranchName = dataClass.Store.Branch.Name,
                            Address = dataClass.Store.Branch.Location,
                            TotalPrice = Convert.ToDecimal(dict["TotalPrice"]),
                            CustomerPay = Convert.ToDecimal(dict["CustomerPay"]),
                            CreatedDate = ((Timestamp)dict["CreatedDate"]).ToDateTime().ToLocalTime(),
                            InvoiceOrderRecords = new ObservableCollection<OrderModel>(Order.OrderRecords.ToList())
                        });

                        Order.OrderRecords.Clear();
                    }    
                    Invoice.InvoiceRecords = _invoice.InvoiceRecords;
                    Page.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddQuantity(string id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity < order.Item.Quantity)
            {
                order.Quantity += 1;
                CalculateSubtotal(order);
                CalculateTotal();
            }
        }

        private void ReduceQuantity(string id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            if (order.Quantity > 1)
            {
                order.Quantity -= 1;
                CalculateSubtotal(order);
                CalculateTotal();
            }
        }

        private void RemoveItem(string id)
        {
            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == id).FirstOrDefault();
            Invoice.InvoiceOrderRecords.Remove(order);
            CalculateTotal();
        }

        private void AddItem()
        {
            InvoiceAddView invoiceAddView = new InvoiceAddView();
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
                InvoicePayView invoicePayView = new InvoicePayView();
                invoicePayView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invoice list is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private async void ConfirmPay(decimal payment)
        {
            InvoiceRepository invoiceRepository = new InvoiceRepository();
            ItemHistoryRepository itemHistoryRepository = new ItemHistoryRepository();
            ItemRepository itemRepository = new ItemRepository();
            DataClass dataClass = DataClass.GetInstance;

            if (payment - Invoice.TotalPrice >= 0)
            {
                Invoice.Id = GenerateId.GenerateInvoice(DateTime.Now);
                Invoice.CustomerPay = payment;
                Invoice.UserFullName = String.Format("{0} {1}", dataClass.LoggedInUser.FName, dataClass.LoggedInUser.LName);
                Invoice.CreatedDate = DateTime.UtcNow;
                MessageBox.Show("Processing payment...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (await invoiceRepository.AddInvoice(Invoice))
                {
                    Invoice.InvoiceRecords.Add(Invoice);
                    foreach (var order in Invoice.InvoiceOrderRecords)
                    {
                        await itemHistoryRepository.AddHistory(order.Item, "TRANSACTION");
                        order.Item.Quantity -= order.Quantity;
                        await itemRepository.UpdateItem(order.Item);                        
                    }
                    GetReceipt(Invoice.InvoiceRecords.Where(i => i.Id == Invoice.Id).FirstOrDefault());                    
                }
            }
            else
            {
                MessageBox.Show("Not enough payment!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClosePayView();
        }

        private void GetReceipt(InvoiceModel invoice)
        {
            invoice.CustomerChange = invoice.CustomerPay - invoice.TotalPrice;
            InvoiceReceiptView invoiceReceiptView = new InvoiceReceiptView(invoice);
            invoiceReceiptView.ShowDialog();
        }

        private void SearchItem(string searchTxt = "")
        {
            Invoice.InvoiceRecords = new ObservableCollection<InvoiceModel>(
                _invoice.InvoiceRecords.Where(i => i.InvoiceOrderRecords.Where(o => i.Id.ToLower().Contains(searchTxt.ToLower())
                || i.UserFullName.ToLower().Contains(searchTxt.ToLower())
                || o.Item.Name.ToLower().Contains(searchTxt.ToLower())
                || o.Item.Brand.ToLower().Contains(searchTxt.ToLower())).Any() == true).ToList());
        }

        public void GetItem(ItemModel item)
        {
            if (item == null)
            {
                MessageBox.Show("Item does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            OrderModel order = Invoice.InvoiceOrderRecords.Where(i => i.ItemId == item.Id).FirstOrDefault();
            InvoiceAddQuantityView invoiceAddQuantityView = new InvoiceAddQuantityView();
            if (invoiceAddQuantityView.ShowDialog() == true)
            {
                if (order == null && invoiceAddQuantityView.Quantity <= item.Quantity)
                {
                    Invoice.InvoiceOrderRecords.Add(new OrderModel()
                    {
                        ItemId = item.Id,
                        Quantity = invoiceAddQuantityView.Quantity,
                        ActualItemPrice = item.Price,
                        SubtotalPrice = item.Price * invoiceAddQuantityView.Quantity,
                        Item = item,
                    });
                    CalculateTotal();
                    MessageBox.Show("Successfully added item to invoice", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (order != null && order.Quantity + invoiceAddQuantityView.Quantity <= item.Quantity)
                {
                    order.Quantity += invoiceAddQuantityView.Quantity;
                    CalculateSubtotal(order);
                    CalculateTotal();
                    MessageBox.Show("Successfully updated item quantity to invoice", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Quantity exceeds actual product quantity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }        

        private void CalculateSubtotal(OrderModel order)
        {
            order.SubtotalPrice = order.Quantity * order.ActualItemPrice;
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
