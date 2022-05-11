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
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand InvoiceViewCommand { get; set; }

        public ScannerViewModel InventoryVM { get; set; }
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
            try
            {
                InventoryVM = new ScannerViewModel();
                InvoiceVM = new InvoiceViewModel();

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
    }
}
