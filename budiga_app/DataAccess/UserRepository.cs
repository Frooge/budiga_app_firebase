using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using budiga_app.Core;
using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;

namespace budiga_app.DataAccess
{
    class UserRepository
    {

        public UserRepository()
        {

        }

        public UserModel GetUser(string username, string password)
        {
            UserModel user = new UserModel();
            

            return user;
        }

        public UserModel GetUserById(int id)
        {
            UserModel user = new UserModel();
            
            return user;
        }

        public ObservableCollection<UserModel> GetAllEmployee()
        {
            ObservableCollection<UserModel> userRecords = new ObservableCollection<UserModel>();
            
            return userRecords;
        }

        public bool AddEmployeeUser(UserModel user)
        {
            bool result = false;
            
            return result;
        }

        public bool UpdateUser(UserModel user)
        {
            bool result = false;
            
            return result;
        }

        public void DeleteUser(int id)
        {

        }
    }

    
}
