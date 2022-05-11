using System;
using System.Collections.Generic;
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
            return user;
        }

        public UserModel GetAll()
        {
            return new UserModel();
        }

        public void AddUser(UserModel user)
        {

        }

        public void UpdateUser(UserModel user)
        {

        }

        public void DeleteUser(int id)
        {

        }
    }

    
}
