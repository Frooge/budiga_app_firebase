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
        private int _id;
        private string _fname;
        private string _lname;
        private string _username;
        private string _password;
        private string _contact;
        private string _userRole;
        private DateTime _created;
        private DateTime? _updated;
        private int _deleted;

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
    }
}
