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
       public RelayCommand AnalyticsViewCommand { get; set; }

        public AnalyticsViewModel AnalyticsVM { get; set; }

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
                AnalyticsVM = new AnalyticsViewModel();

                CurrentView = AnalyticsVM;

                AnalyticsViewCommand = new RelayCommand(o =>
                {
                    CurrentView = AnalyticsVM;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }
    }
}
