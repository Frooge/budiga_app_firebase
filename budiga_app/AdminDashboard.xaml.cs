using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;

namespace budiga_app
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private AttendanceModel attendance;
        private AttendanceRepository attendanceRepository;
        public AdminDashboard()
        {
            InitializeComponent();
            attendance = new AttendanceModel()
            {
                UserId = Sessions.session.Id,
                TimeIn = DateTime.Now,
            };            
            userName.Text = Sessions.session.Username;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Sessions.Dispose();
            this.Hide();
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attendanceRepository = new AttendanceRepository();
            attendanceRepository.AddAttendance(attendance);
        }
    }
}
