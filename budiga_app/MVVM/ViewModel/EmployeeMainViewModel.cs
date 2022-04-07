﻿using budiga_app.Core;
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
        public ScannerViewModel ScanVM { get; set; }

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
                CurrentView = ScanVM;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            
        }
    }
}
