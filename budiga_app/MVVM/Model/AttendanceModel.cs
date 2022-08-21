using budiga_app.Core;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace budiga_app.MVVM.Model
{
    public class AttendanceModel : ObservableObject
    {
        private string _id;
        private string _userFullName;
        private DateTime _timeIn;
        private DateTime? _timeOut;
        private ObservableCollection<AttendanceModel> attendanceRecords;

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string UserFullName { get { return _userFullName; } set { _userFullName = value; OnPropertyChanged("UserFullName"); } }
        public DateTime TimeIn { get { return _timeIn; } set { _timeIn = value; OnPropertyChanged("TimeIn"); } }
        public DateTime? TimeOut { get { return _timeOut; } set { _timeOut = value; OnPropertyChanged("TimeOut"); } }
        public ObservableCollection<AttendanceModel> AttendanceRecords { get { return attendanceRecords; } set { attendanceRecords = value; OnPropertyChanged("AttendanceRecords"); } }
        private void AttendanceRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("AttendanceRecords");
        }

    }
}
