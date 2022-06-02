using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class AttendanceRepository
    {

        public AttendanceRepository()
        {

        }

        public ObservableCollection<AttendanceModel> GetAllAttendance()
        {
            ObservableCollection<AttendanceModel> attendanceRecords = new ObservableCollection<AttendanceModel>();
            
            return attendanceRecords;
        }

        public bool AddAttendance(AttendanceModel attendance)
        {
            bool result = false;
            
            return result;
        }
    }
}
