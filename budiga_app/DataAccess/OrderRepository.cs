﻿using budiga_app.MVVM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class OrderRepository
    {
        private dbConn database;
        
        public OrderRepository(dbConn db)
        {
            database = db;
        }

        public bool AddOrder(int invoiceId, OrderModel order)
        {
            bool result = false;
            string query = string.Format("INSERT INTO order (item_id, invoice_id, quantity, subtotal_price) VALUES ('{0}', '{1}', '{2}', '{3}')", order.ItemId, invoiceId, order.Quantity, order.SubtotalPrice);
            try
            {
                database.Connection();
                MySqlCommand commandDatabase = new MySqlCommand(query, database.conn);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.ExecuteReader();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }
    }
}