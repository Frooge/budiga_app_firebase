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
    public class AttendanceModel : ObservableObject
    {
        private int _id;
        private int _userId;
        private DateTime _timeIn;
        private DateTime _timeOut;
        private UserModel _user;
        private ObservableCollection<AttendanceModel> attendanceRecords;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public int UserId { get { return _userId; } set { _userId = value; OnPropertyChanged("UserId"); } }
        public DateTime TimeIn { get { return _timeIn; } set { _timeIn = value; OnPropertyChanged("TimeIn"); } }
        public DateTime TimeOut { get { return _timeOut; } set { _timeOut = value; OnPropertyChanged("TimeOut"); } }
        public UserModel User { get { return _user; } set { _user = value; OnPropertyChanged("User"); } }
        public ObservableCollection<AttendanceModel> AttendanceRecords { get { return attendanceRecords; } set { attendanceRecords = value; OnPropertyChanged("AttendanceRecords"); } }
        private void AttendanceRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("AttendanceRecords");
        }

    }
}
