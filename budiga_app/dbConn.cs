using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app
{
    class dbConn
    {
        public static MySqlConnection conn;
        public void connection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=budiga_app;";
            conn = new MySqlConnection(connectionString);
            
            try
            {
                conn.Open(); 
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public void dispose()
        {
            conn.Close();
        }
    }
}
