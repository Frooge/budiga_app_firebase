using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app
{
    public class dbConn
    {
        private string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=budiga_app;max pool size=5;";
        public MySqlConnection conn { get; set; }

        public void Connection() //try fork 2
        {
            conn = new MySqlConnection(connectionString);            
            try
            {
                conn.Open(); 
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public void Dispose()
        {
            conn.Close();
        }
    }
}
