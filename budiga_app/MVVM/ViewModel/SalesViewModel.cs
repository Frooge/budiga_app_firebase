using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace budiga_app.MVVM.ViewModel
{
    class SalesViewModel : ObservableObject
    {
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public SalesOverviewViewModel SalesOverviewVM { get; set; }
        public SalesInventoryViewModel SalesInventoryVM { get; set; }

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
        }
    }
}
