using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class EmployeeViewModel
    {
        private UserRepository userRepository;
        private AttendanceRepository attendanceRepository;
        public AttendanceModel Attendance { get; set; }
        public UserModel User { get; set; }
        public RelayCommand AddEmployeeCommand { get; set; }
        public RelayCommand EditEmployeeCommand { get; set; }

        public EmployeeViewModel()
        {
            User = new UserModel();
            Attendance = new AttendanceModel();
            userRepository = new UserRepository();
            attendanceRepository = new AttendanceRepository();
            AddEmployeeCommand = new RelayCommand(param => AddEmployee());
            EditEmployeeCommand = new RelayCommand(param =>EditEmployee((UserModel)param));
            GetAllEmployee();
            GetAllAttendance();
        }

        private void AddEmployee()
        {
            EmployeeAddView employeeAddView = new EmployeeAddView(this);
            employeeAddView.ShowDialog();
        }

        private void EditEmployee(UserModel user)
        {
            EmployeeEditView employeeEditView = new EmployeeEditView(user, this);
            employeeEditView.ShowDialog();
        }

        public void GetAllEmployee()
        {
            User.UserRecords = userRepository.GetAllEmployee();
        }

        private void GetAllAttendance()
        {
            Attendance.AttendanceRecords = attendanceRepository.GetAllAttendance();
        }
    }
}
