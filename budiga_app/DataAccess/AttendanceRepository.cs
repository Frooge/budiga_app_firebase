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
        private dbConn database;
        private UserRepository userRepository;

        public AttendanceRepository()
        {
            database = new dbConn();
            userRepository = new UserRepository();
        }

        public ObservableCollection<AttendanceModel> GetAllAttendance()
        {
            ObservableCollection<AttendanceModel> attendanceRecords = new ObservableCollection<AttendanceModel>();
            string query = "SELECT * FROM `attendance` ORDER BY `id` DESC";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    attendanceRecords.Add(new AttendanceModel()
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetInt32("user_id"),
                        TimeIn = reader.GetDateTime("time_in"),
                        TimeOut = reader.GetDateTime("time_out"),
                        User = userRepository.GetUserById(reader.GetInt32("user_id")),
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                database.Dispose();
            }
            return attendanceRecords;
        }

        public bool AddAttendance(AttendanceModel attendance)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `attendance` (`user_id`, `time_in`, `time_out`) VALUES ('{0}', '{1}', '{2}')", attendance.UserId, attendance.TimeIn.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                database.Dispose();
            }
            return result;
        }
    }
}
