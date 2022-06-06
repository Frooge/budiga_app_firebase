using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class AdminMainViewModel : ObservableObject
    {
        public DataClass Data { get; set; }
        public string Username { get; set; }

        public RelayCommand InventoryViewCommand { get; set; }        
        public RelayCommand InvoiceViewCommand { get; set; }
        public RelayCommand SalesViewCommand { get; set; }
        public RelayCommand EmployeeViewCommand { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public InvoiceViewModel InvoiceVM { get; set; }
        public SalesViewModel SalesVM { get; set; }     
        public EmployeeViewModel EmployeeVM { get; set; }

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

        public AdminMainViewModel()
        {
            Initialize();

            try
            {
                InventoryVM = InventoryViewModel.GetInstance;
                InvoiceVM = InvoiceViewModel.GetInstance;
                SalesVM = new SalesViewModel();                                
                EmployeeVM = EmployeeViewModel.GetInstance;

                CurrentView = InventoryVM;

                InventoryViewCommand = new RelayCommand(o =>
                {
                    CurrentView = InventoryVM;
                });

                InvoiceViewCommand = new RelayCommand(o =>
                {
                    CurrentView = InvoiceVM;
                });

                SalesViewCommand = new RelayCommand(o =>
                {
                    SalesViewModel.GetInstance.ResetValues();
                    CurrentView = SalesVM;
                });

                EmployeeViewCommand = new RelayCommand(o =>
                {
                    CurrentView = EmployeeVM;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }

        private void Initialize()
        {
            Data = DataClass.GetInstance;
            Username = string.Format("{0} {1} | {2}", Data.LoggedInUser.FName, Data.LoggedInUser.LName, Data.LoggedInUser.Type);
        }
    }
}
