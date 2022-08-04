using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using budiga_app.MVVM.Model;
using budiga_app.DataAccess;
using System.Windows;
using Google.Cloud.Firestore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class SalesViewModel : ObservableObject
    {
        private static SalesViewModel _instance;           
        private OverviewSalesModel _overviewSales;
        private InventorySalesModel _inventorySales;
        private InvoiceViewModel invoiceVM;


        public SalesOverviewViewModel SalesOverviewVM { get; set; }
        public SalesInventoryViewModel SalesInventoryVM { get; set; }
        public TotalSalesModel Total { get; set; }
        public OverviewSalesModel OverviewSales { get; set; }      
        public InventorySalesModel InventorySales { get; set; }
        public PageModel Page { get; set; }
        public RelayCommand ChangePeriodCommand { get; set; }
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        

        public static SalesViewModel GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SalesViewModel();
                }
                return _instance;
            }
        }

        public static void ReleaseInstance()
        {
            _instance = null;
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
            
        }


        public SalesViewModel()
        {
            Initialize();

            try
            {
                SalesOverviewVM = new SalesOverviewViewModel();
                SalesInventoryVM = new SalesInventoryViewModel();

                CurrentView = SalesOverviewVM;

                OverviewViewCommand = new RelayCommand(o =>
                {
                    CurrentView = SalesOverviewVM;
                });

                InventoryViewCommand = new RelayCommand(o =>
                {
                    CurrentView = SalesInventoryVM;
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

            ChangePeriodCommand = new RelayCommand(param => ChangePeriod((string)param));
        }
        
        private void Initialize()
        {
            Total = new TotalSalesModel();
            _overviewSales = new OverviewSalesModel();
            OverviewSales = new OverviewSalesModel();
            _inventorySales = new InventorySalesModel();
            InventorySales = new InventorySalesModel();
            invoiceVM = InvoiceViewModel.GetInstance;
            Page = new PageModel();
            // GetAll();
        }

        private void GetAll() // not used yet
        {            
            try
            {
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("invoice");
                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    if(invoiceVM.Invoice.InvoiceRecords != null)
                    {
                        ResetValues();
                    }                    
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ResetValues()
        {
            ChangePeriod("accumulated");
            OverviewViewCommand.Execute(null);
        }

        private void ChangePeriod(string date)
        {
            Total.Sales = 0;
            Total.Transactions = 0;
            switch (date)
            {
                case "accumulated":
                    GetPeriod(date, (invoice) =>
                    {
                        Total.Transactions++;
                        Total.Sales += invoice.TotalPrice;
                    });
                    break;
                case "daily":
                    GetPeriod(date, (invoice) =>
                    {
                        if (invoice.CreatedDate.Date == DateTime.Now.Date)
                        {
                            Total.Transactions++;
                            Total.Sales += invoice.TotalPrice;
                        }
                    });
                    break;
                case "monthly":
                    GetPeriod(date, (invoice) =>
                    {
                        if (invoice.CreatedDate.Year.ToString() + invoice.CreatedDate.Month.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString())
                        {
                            Total.Transactions++;
                            Total.Sales += invoice.TotalPrice;
                        }
                    });
                    break;
                case "annually":
                    GetPeriod(date, (invoice) =>
                    {
                        if (invoice.CreatedDate.Year == DateTime.Now.Year)
                        {
                            Total.Transactions++;
                            Total.Sales += invoice.TotalPrice;
                        }
                    });
                    break;
                default:
                    break;
            }
        }

        private void GetPeriod(string period, Action<InvoiceModel> GetTotal)
        {
            DataClass dataClass = DataClass.GetInstance;
            _overviewSales.OverviewSalesRecords = new ObservableCollection<OverviewSalesModel>();
            _inventorySales.InventorySalesRecords = new ObservableCollection<InventorySalesModel>();
            if(invoiceVM.Invoice.InvoiceRecords != null)
            {
                foreach (var invoice in invoiceVM.Invoice.InvoiceRecords)
                {
                    foreach (var order in invoice.InvoiceOrderRecords)
                    {
                        var inventorySales = _inventorySales.InventorySalesRecords.Where(i => i.Item.Id == order.Item.Id && i.Date == GetDate(invoice.CreatedDate, period)).FirstOrDefault();
                        if (inventorySales == null)
                        {
                            _inventorySales.InventorySalesRecords.Add(new InventorySalesModel
                            {
                                UnitsSold = order.Quantity,
                                TotalSales = order.SubtotalPrice,
                                Item = order.Item,
                                Date = GetDate(invoice.CreatedDate, period)
                            });
                        }
                        else
                        {
                            inventorySales.UnitsSold += order.Quantity;
                            inventorySales.TotalSales += order.SubtotalPrice;
                        }

                        var overviewSales = _overviewSales.OverviewSalesRecords.Where(o => o.Date == GetDate(invoice.CreatedDate, period)).FirstOrDefault();
                        if (overviewSales == null)
                        {
                            _overviewSales.OverviewSalesRecords.Add(new OverviewSalesModel
                            {
                                UnitsSold = order.Quantity,
                                Total = order.SubtotalPrice,
                                Date = GetDate(invoice.CreatedDate, period)
                            });
                        }
                        else
                        {
                            overviewSales.UnitsSold += order.Quantity;
                            overviewSales.Total += order.SubtotalPrice;
                        }
                    }
                    GetTotal(invoice);
                }
                InventorySales.InventorySalesRecords = _inventorySales.InventorySalesRecords;
                OverviewSales.OverviewSalesRecords = _overviewSales.OverviewSalesRecords;
                Page.IsLoading = false;
            }
            else
            {
                Page.IsLoading = true;
            }
        }

        private string GetDate(DateTime date, string period)
        {
            switch (period)
            {
                case "accumulated":
                    return "N/A";
                case "daily":
                    return string.Format("{0}/{1}/{2}", date.Month, date.Day, date.Year);
                case "monthly":
                    return string.Format("{0}/{1}", date.Month, date.Year);
                case "annually":
                    return date.Year.ToString();
                default:
                    return string.Empty;
            }
        }
    }
}
