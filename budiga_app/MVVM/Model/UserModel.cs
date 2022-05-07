using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using budiga_app.Core;

namespace budiga_app.MVVM.Model
{
    public class UserModel : ObservableObject
    {
        int _id;
        string _fname;
        string _lname;
        string _username;
        string _password;
        string _contact;
        string _userRole;
        DateTime _created;
        DateTime? _updated;
        int _deleted;
        ObservableCollection<UserModel> _userRecords;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string FName { get { return _fname; } set { _fname = value; OnPropertyChanged("FName"); } }
        public string LName { get {  return _lname; } set { _lname = value; OnPropertyChanged("LName"); } }
        public string UserName { get { return _username; } set { _username = value; OnPropertyChanged("UserName"); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        public string Contact { get { return _contact; } set { _contact = value; OnPropertyChanged("Contact"); } }
        public string UserRole { get { return _userRole; } set {_userRole = value; OnPropertyChanged("UserRole"); } }
        public DateTime Created { get { return _created; } set { _created = value; OnPropertyChanged("Created"); } }
        public DateTime? Updated { get { return _updated; } set { _updated = value; OnPropertyChanged("Update"); } }
        public int Deleted { get { return _deleted; } set { _deleted = value; OnPropertyChanged("Deleted"); } }
        public ObservableCollection<UserModel> UserRecords { get { return _userRecords; } set { _userRecords = value; OnPropertyChanged("UserRecords"); } }

        private void UserModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("UserModel");
        }
    }
}
