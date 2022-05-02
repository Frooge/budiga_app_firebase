using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace budiga_app.MVVM.ViewModel
{
    class StatisticsViewModel : ObservableObject
    {
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public OverviewViewModel OverviewVM { get; set; }
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

        public StatisticsViewModel()
        {
            try
            {
                OverviewVM = new OverviewViewModel();
                SalesInventoryVM = new SalesInventoryViewModel();

                CurrentView = OverviewVM;

                OverviewViewCommand = new RelayCommand(o =>
                {
                    CurrentView = OverviewVM;
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
