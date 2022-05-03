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
        private UserModel _user;

        public UserRepository()
        {
            database = new dbConn();
            _user = new UserModel();
        }

        public UserModel GetUser(string username, string password)
        {
            string query = "SELECT * FROM users WHERE username = '" + username + "' AND password = '" + password + "'";
            MySqlDataReader reader;
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    var results = new object[reader.FieldCount];
                    while (reader.Read())
                    {
                        reader.GetValues(results);
                    }
                    results.ToArray();
                    if (results.Length > 0)
                    {
                        _user.Id = int.Parse(results[0].ToString());
                        _user.FName = results[1].ToString();
                        _user.LName = results[2].ToString();
                        _user.UserName = results[3].ToString();
                        _user.Password = results[4].ToString();
                        _user.Contact = results[5].ToString();
                        _user.UserRole = results[6].ToString();
                        _user.Created = DateTime.Parse(results[7].ToString());
                        _user.Updated = DateTime.Parse(results[8].ToString());
                        _user.Deleted = int.Parse(results[9].ToString());
                    }
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

            return _user;
        }
        public UserModel Get(int id)
        {
            return new UserModel();
        }

        public UserModel GetAll()
        {
            return new UserModel();
        }

        public void Add(UserModel user)
        {

        }

        public void Update(UserModel user)
        {

        }

        public void delete(int id)
        {

        }
    }

    
}
