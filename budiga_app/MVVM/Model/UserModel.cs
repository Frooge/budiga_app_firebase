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
        private string _id;
        private string _fname;
        private string _lname;
        private string _username;
        private string _password;
        private string _contact;
        private string _type;
        private DateTime _createdDate;
        private ObservableCollection<UserModel> _userRecords;

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string FName { get { return _fname; } set { _fname = value; OnPropertyChanged("FName"); } }
        public string LName { get {  return _lname; } set { _lname = value; OnPropertyChanged("LName"); } }
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged("Username"); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        public string Contact { get { return _contact; } set { _contact = value; OnPropertyChanged("Contact"); } }
        public string Type { get { return _type; } set {_type = value; OnPropertyChanged("Type"); } }
        public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; OnPropertyChanged("CreatedDate"); } }
        public ObservableCollection<UserModel> UserRecords { get { return _userRecords; } set { _userRecords = value; OnPropertyChanged("UserRecords"); } }
        private void UserRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("UserRecords");
        }
    }
}


