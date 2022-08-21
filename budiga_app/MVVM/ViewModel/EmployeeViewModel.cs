using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.MVVM.ViewModel
{
    public class EmployeeViewModel
    {
        private static EmployeeViewModel _instance;
        private UserRepository userRepository;        

        private UserModel _employee;
        public UserModel Employee { get; set; }
        public DataClass Data { get; set; }
        public AttendanceModel Attendance { get; set; }
        public RelayCommand AddEmployeeModalCommand { get; set; }
        public RelayCommand EditEmployeeModalCommand { get; set; }

        public static EmployeeViewModel GetInstance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new EmployeeViewModel();
                }
                return _instance; 
            }
        }

        public static void ReleaseInstance()
        {
            _instance = null;
        }

        public EmployeeViewModel()
        {
            Initialize();
            GetAllEmployee();
            GetAllAttendance();
        }

        private void Initialize()
        {
            Data = DataClass.GetInstance;
            Employee = new UserModel();
            _employee = new UserModel();
            Attendance = new AttendanceModel();
            userRepository = new UserRepository();
            AddEmployeeModalCommand = new RelayCommand(param => AddEmployeeModal());
            EditEmployeeModalCommand = new RelayCommand(param => EditEmployeeModal((UserModel)param));
        }

        public void GetAllEmployee()
        {
            try
            {
                DataClass dataClass = DataClass.GetInstance;
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("Users").WhereEqualTo("Type", "employee").WhereEqualTo("StoreId", Data.LoggedInUser.StoreId);

                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    _employee.UserRecords = new ObservableCollection<UserModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            _employee.UserRecords.Add(new UserModel()
                            {
                                Id = dict["Id"].ToString(),
                                StoreId = dict["StoreId"].ToString(),
                                FName = dict["FName"].ToString(),
                                LName = dict["LName"].ToString(),
                                Username = dict["Username"].ToString(),
                                Password = dict["Password"].ToString(),
                                Contact = dict["Contact"].ToString(),
                                Online = Convert.ToBoolean(dict["Online"].ToString())
                            });
                        });
                    }
                    Employee.UserRecords = _employee.UserRecords;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllAttendance()
        {
            try
            {
                DataClass dataClass = DataClass.GetInstance;
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Attendance").OrderByDescending("TimeIn").Limit(10);

                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    Attendance.AttendanceRecords = new ObservableCollection<AttendanceModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            Attendance.AttendanceRecords.Add(new AttendanceModel()
                            {
                                Id = dict["Id"].ToString(),
                                UserFullName = dict["UserFullName"].ToString(),
                                TimeIn = ((Timestamp)dict["TimeIn"]).ToDateTime().ToLocalTime()
                            });
                            if (dict["TimeOut"] == null)
                                Attendance.AttendanceRecords.Last().TimeOut = null;
                            else
                                Attendance.AttendanceRecords.Last().TimeOut = ((Timestamp)dict["TimeOut"]).ToDateTime().ToLocalTime();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEmployeeModal()
        {
            EmployeeAddView employeeAddView = new EmployeeAddView();
            employeeAddView.ShowDialog();
        }

        private void EditEmployeeModal(UserModel user)
        {
            EmployeeEditView employeeEditView = new EmployeeEditView(user);
            employeeEditView.ShowDialog();
        }       
    }
}
