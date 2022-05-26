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
        private dbConn database;

        public UserRepository()
        {
            database = new dbConn();
        }

        public UserModel GetUser(string username, string password)
        {
            UserModel user = new UserModel();
            string query = String.Format("SELECT * FROM `users` WHERE `username` = '{0}' AND `password` = '{1}' AND `is_deleted` = 0", username, password); ;
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    user = new UserModel()
                    {
                        Id = reader.GetInt32("id"),
                        FName = reader.GetString("fname"),
                        LName = reader.GetString("lname"),
                        Username = reader.GetString("username"),
                        Password = reader.GetString("password"),
                        Contact = reader.GetString("contact"),
                        UserRole = reader.GetString("user_role"),
                        CreatedDate = reader.GetDateTime("created_date"),
                        UpdatedDate = reader.GetDateTime("updated_date")
                    };
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

            return user;
        }

        public UserModel GetUserById(int id)
        {
            UserModel user = new UserModel();
            string query = String.Format("SELECT * FROM `users` WHERE `id` = {0}", id);
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    user = new UserModel()
                    {
                        Id = reader.GetInt32("id"),
                        FName = reader.GetString("fname"),
                        LName = reader.GetString("lname"),
                        Username = reader.GetString("username"),
                        Password = reader.GetString("password"),
                        Contact = reader.GetString("contact"),
                        UserRole = reader.GetString("user_role"),
                        CreatedDate = reader.GetDateTime("created_date"),
                        UpdatedDate = reader.GetDateTime("updated_date")
                    };
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
            return user;
        }

        public ObservableCollection<UserModel> GetAllEmployee()
        {
            ObservableCollection<UserModel> userRecords = new ObservableCollection<UserModel>();
            string query = "SELECT * FROM `users` WHERE `user_role` = 'Employee'";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    userRecords.Add(new UserModel()
                    {
                        Id = reader.GetInt32("id"),
                        FName = reader.GetString("fname"),
                        LName = reader.GetString("lname"),
                        Username = reader.GetString("username"),
                        Password = reader.GetString("password"),
                        Contact = reader.GetString("contact"),
                        UserRole = reader.GetString("user_role"),
                        CreatedDate = reader.GetDateTime("created_date"),
                        UpdatedDate = reader.GetDateTime("updated_date")
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
            return userRecords;
        }

        public bool AddEmployeeUser(UserModel user)
        {
            bool result = false;
            string query = string.Format("INSERT INTO `users` (`fname`, `lname`, `username`, `password`, `contact`, `user_role`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", user.FName, user.LName, user.Username, user.Password, user.Contact, user.UserRole);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully added employee", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public bool UpdateUser(UserModel user)
        {
            bool result = false;
            string query = string.Format("UPDATE `users` SET `fname`='{0}',`lname`='{1}',`username`='{2}',`password`='{3}',`contact`='{4}',`updated_date`='{5}' WHERE id = '{6}'", user.FName, user.LName, user.Username, user.Password, user.Contact, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user.Id);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                MessageBox.Show("Successfully updated employee", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public void DeleteUser(int id)
        {

        }
    }

    
}
