using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
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
        public UserModel User { get; set; }
        public EmployeeViewModel()
        {
            User = new UserModel();
            userRepository = new UserRepository();
            GetAll();
        }

        private void GetAll()
        {
            User.UserRecords = userRepository.GetAllEmployee();
        }
    }
}
