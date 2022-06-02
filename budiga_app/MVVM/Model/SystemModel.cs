using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM
{
    public class SystemModel : ObservableObject
    {
        private bool _isLoading;
        public bool IsLoading { get { return _isLoading; } set { _isLoading = value; OnPropertyChanged("IsLoading"); } }
    }
}
