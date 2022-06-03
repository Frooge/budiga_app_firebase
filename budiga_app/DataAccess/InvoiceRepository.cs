using budiga_app.Core;
using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class InvoiceRepository
    {
        private FirestoreConn conn;
        private DataClass dataClass;
        public InvoiceRepository()
        {
            conn = FirestoreConn.GetInstance;
            dataClass = DataClass.GetInstance;
        }

        public async Task<bool> AddInvoice(InvoiceModel invoice)
        {
            bool result = false;
            try
            {
                WriteBatch batch = conn.FirestoreDb.StartBatch();
                Dictionary<string, object> invoiceDict = new Dictionary<string, object>
                {
                    { "Id", invoice.Id },
                    { "UserFullName", invoice.UserFullName },
                    { "StoreId", invoice.StoreId },
                    { "TotalPrice", Convert.ToDouble(invoice.TotalPrice) },
                    { "CustomerPay", Convert.ToDouble(invoice.CustomerPay) },
                    { "CreatedDate", DateTime.UtcNow },
                };
                DocumentReference invoiceRef = conn.FirestoreDb.Collection("invoice").Document(invoice.Id);
                batch.Set(invoiceRef, invoiceDict);
                
                foreach(var order in invoice.InvoiceOrderRecords)
                {
                    string newOrderId = GenerateId.GenerateCommon();
                    Dictionary<string, object> orderDict = new Dictionary<string, object>
                    {
                        { "Id", newOrderId },
                        { "ItemId", order.ItemId },
                        { "InvoiceId", invoice.Id },
                        { "Quantity", order.Quantity },
                        { "ActualItemPrice", Convert.ToDouble(order.ActualItemPrice) },
                        { "SubtotalPrice", Convert.ToDouble(order.SubtotalPrice) }
                    };
                    DocumentReference orderRef = conn.FirestoreDb.Collection("orders").Document(newOrderId);

                    batch.Set(orderRef, orderDict);
                }

                await batch.CommitAsync();

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
