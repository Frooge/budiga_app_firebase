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
    class UserModel : ObservableObject
    {
        private int _id;
        public int Id 
        { 
            get 
            { 
                return _id; 
            } 
            set 
            {
                _id = value;
                OnPropertyChanged("Id");
            } 
        }

        private string _fname;
        public string FName
        {
            get
            {
                return _fname;
            }
            set
            {
                _fname = value;
                OnPropertyChanged("FName");
            }
        }

        private string _lname;
        public string LName 
        { 
            get 
            { 
                return _lname; 
            } 
            set 
            {
                _lname = value; 
                OnPropertyChanged("LName"); 
            } 
        }

        private string _username;
        public string UserName 
        {
            get 
            { 
                return _username; 
            } 
            set 
            { 
                _username = value; 
                OnPropertyChanged("UserName");
            } 
        }

        private string _password;
        public string Password 
        { 
            get 
            { 
                return _password; 
            } 
            set 
            { 
                _password = value; 
                OnPropertyChanged("Password"); 
            } 
        }

        private string _contact;
        public string Contact 
        {
            get {
                return _contact; 
            } 
            set 
            { 
                _contact = value; 
                OnPropertyChanged("Contact"); 
            } 
        }

        private string _userRole;
        public string UserRole 
        { 
            get 
            { 
                return _userRole; 
            } 
            set 
            {
                _userRole = value;
                OnPropertyChanged("UserRole"); 
            } 
        }

        private DateTime _created;
        public DateTime Created
        { get 
            { 
                return _created; 
            } 
            set 
            {
                _created = value; 
                OnPropertyChanged("Created"); 
            } 
        }

        private DateTime? _updated;
        public DateTime? Updated {
            get
            { 
                return _updated;
            } 
            set 
            {
                _updated = value;
                OnPropertyChanged("Update");
            }
        }

        private int _deleted;
        public int Deleted 
        { 
            get 
            { 
                return _deleted; 
            } 
            set 
            { 
                _deleted = value; 
                OnPropertyChanged("Deleted"); 
            }
        }

        private ObservableCollection<UserModel> _userRecords;
        public ObservableCollection<UserModel> UserRecords 
        {
            get
            {
                return _userRecords; 
            } 
            set 
            { 
                _userRecords = value;
                OnPropertyChanged("UserRecords"); 
            } 
        }

        private void StudentModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("UserModel");
        }
    }
}
