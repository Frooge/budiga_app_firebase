using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    class AdminMainViewModel : ObservableObject
    {
        public RelayCommand StatisticsViewCommand { get; set; }
        public RelayCommand AnalyticsViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand EmployeeViewCommand { get; set; }
        public StatisticsViewModel StatisticsVM { get; set; }
        public AnalyticsViewModel AnalyticsVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
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
            try
            {
                StatisticsVM = new StatisticsViewModel();
                AnalyticsVM = new AnalyticsViewModel();
                InventoryVM = new InventoryViewModel();
                EmployeeVM = new EmployeeViewModel();

                CurrentView = StatisticsVM;

                StatisticsViewCommand = new RelayCommand(o =>
                {
                    CurrentView = StatisticsVM;
                });

                AnalyticsViewCommand = new RelayCommand(o =>
                {
                    CurrentView = AnalyticsVM;
                });

                InventoryViewCommand = new RelayCommand(o =>
                {
                    CurrentView = InventoryVM;
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
    }
}
