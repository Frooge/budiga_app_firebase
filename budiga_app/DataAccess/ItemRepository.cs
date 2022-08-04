using budiga_app.Core;
using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class ItemRepository
    {
        private FirestoreConn conn ;
        private DataClass dataClass;
        public ItemRepository()
        {
            conn = FirestoreConn.GetInstance;
            dataClass = DataClass.GetInstance;
        }

        public ItemModel GetItem(int id)
        {
            ItemModel item = new ItemModel();
            
            return item;
        }

        public ItemModel GetItemByBarcode(string barcode)
        {
            ItemModel item = new ItemModel();
            
            return item;
        }

        public async Task<bool> AddItem(ItemModel item)
        {
            bool result = false;
            try
            {                
                DocumentReference docRef = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Items").Document(item.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", item.Id },
                    { "Barcode", item.Barcode },
                    { "Name", item.Name },
                    { "Brand", item.Brand },
                    { "Price", Convert.ToDouble(item.Price) },
                    { "Quantity", item.Quantity },
                    { "IsDeleted", item.IsDeleted },
                };
                await docRef.SetAsync(dict);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task<bool> UpdateItem(ItemModel item)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Items").Document(item.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Barcode", item.Barcode },
                    { "Name", item.Name },
                    { "Brand", item.Brand },
                    { "Price", (double)item.Price },
                    { "Quantity", item.Quantity },
                };
                await docRef.UpdateAsync(dict);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task<bool> DeleteItem(string id)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(dataClass.Store.Branch.Id).Collection("Items").Document(id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "IsDeleted", true }
                };
                await docRef.SetAsync(dict, SetOptions.MergeAll);
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
