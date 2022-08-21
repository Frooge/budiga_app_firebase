using budiga_app.Core;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace budiga_app.MVVM.Model
{
    public class StoreModel : ObservableObject
    {
        private string _id;
        private string _name;
        private BranchModel _branch;
        private ObservableCollection<BranchModel> _branchRecords;

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public BranchModel Branch { get { return _branch; } set { _branch = value; OnPropertyChanged("Branch"); } }
        public ObservableCollection<BranchModel> BranchRecords { get { return _branchRecords; } set { _branchRecords = value; OnPropertyChanged("BranchRecords"); } }
        private void StoreRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("BranchRecords");
        }

        public class BranchModel : ObservableObject
        {
            private string _id;
            private string _name;
            private string _location;

            public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
            public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
            public string Location { get { return _location; } set { _location = value; OnPropertyChanged("Location"); } }

        }
    }
}
