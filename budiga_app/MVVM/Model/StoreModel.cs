using budiga_app.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.Model
{
    public class StoreModel : ObservableObject
    {
        private string _id;
        private string _name;
        private string _location;
        private ObservableCollection<StoreModel> _storeRecords;

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Location { get { return _location; } set { _location = value; OnPropertyChanged("Location"); } }
        public ObservableCollection<StoreModel> StoreRecords { get { return _storeRecords; } set { _storeRecords = value; OnPropertyChanged("StoreRecords"); } }
        private void StoreRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("StoreRecords");
        }
    }
}
