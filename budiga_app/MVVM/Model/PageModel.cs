using budiga_app.Core;

namespace budiga_app.MVVM
{
    public class PageModel : ObservableObject
    {
        private bool _isLoading;
        public bool IsLoading { get { return _isLoading; } set { _isLoading = value; OnPropertyChanged("IsLoading"); } }
    }
}
