using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class EmployeeMainViewModel : ObservableObject
    {
        public DataClass Data { get; set; }
        public string Username { get; set; }
        public string Store { get; set; }

        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand InvoiceViewCommand { get; set; }

        public InventoryViewModel InventoryVM { get; set; }
        public InvoiceViewModel InvoiceVM { get; set; }

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

        public EmployeeMainViewModel()
        {
            Initialize();

            try
            {
                InventoryVM = InventoryViewModel.GetInstance;
                InvoiceVM = InvoiceViewModel.GetInstance;

                CurrentView = InventoryVM;

                InventoryViewCommand = new RelayCommand(o =>
                {
                    CurrentView = InventoryVM;
                });

                InvoiceViewCommand = new RelayCommand(o =>
                {
                    CurrentView = InvoiceVM;
                });

            } catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            
        }

        private void Initialize()
        {
            Data = DataClass.GetInstance;
            Username = string.Format("{0} {1} | {2}", Data.LoggedInUser.FName, Data.LoggedInUser.LName, Data.LoggedInUser.Type);
            Store = string.Format("{0} | {1}", Data.Store.Branch.Location, Data.LoggedInUser.StoreId);
        }
    }
}
