using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    class EmployeeMainViewModel : ObservableObject
    {
        public RelayCommand ScannerViewCommand { get; set; }
        public RelayCommand InvoiceViewCommand { get; set; }

        public ScannerViewModel ScanVM { get; set; }
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
                ScanVM = new ScannerViewModel();
                InvoiceVM = new InvoiceViewModel();

                CurrentView = InvoiceVM;

                ScannerViewCommand = new RelayCommand(o =>
                {
                    CurrentView = ScanVM;
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
