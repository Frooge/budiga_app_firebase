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
    class ItemHistoryRepository
    {
        private DataClass dataClass;
        private FirestoreConn conn;
        public ItemHistoryRepository()
        {
            dataClass = DataClass.GetInstance;
            conn = FirestoreConn.GetInstance;
        }

        public async Task<bool> AddHistory(ItemModel item, string action)
        {
            bool result = false;
            try
            {
                string newId = GenerateId.GenerateCommon();
                DocumentReference docRef = conn.FirestoreDb.Collection("item_history").Document(newId);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", newId },
                    { "StoreId", dataClass.Store.Id },
                    { "BranchId", dataClass.Store.Branch.Id },
                    { "ItemId", item.Id},
                    { "UserFullName", string.Format("{0} {1}", dataClass.LoggedInUser.FName, dataClass.LoggedInUser.LName) },
                    { "Barcode", item.Barcode },
                    { "Name", item.Name },
                    { "Brand", item.Brand },
                    { "Price", (double)item.Price },
                    { "Quantity", item.Quantity },
                    { "Action", action},
                    { "CommittedDate", DateTime.Now.ToUniversalTime() },
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

        public async Task<bool> DeleteHistory(string id)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("item_history").Document(id);
                await docRef.DeleteAsync();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task<bool> UndoAction(ItemHistoryModel itemHistory)
        {
            bool result = false;
            try
            {
                string newAction;
                WriteBatch batch = conn.FirestoreDb.StartBatch();
                DocumentReference itemRef = conn.FirestoreDb.Collection("items").Document(itemHistory.ItemId);
                Dictionary<string, object> dict;
                switch (itemHistory.Action)
                {
                    case "UPDATED": case "MODIFIED": case "TRANSACTION" :
                        dict = new Dictionary<string, object>
                        {
                            { "Id", itemHistory.ItemId },
                            { "Barcode", itemHistory.Barcode },
                            { "Name", itemHistory.Name },
                            { "Brand", itemHistory.Brand },
                            { "Price", (double)itemHistory.Price },
                            { "Quantity", itemHistory.Quantity },
                        };
                        newAction = "MODIFIED";
                        break;
                    case "ADDED":
                        dict = new Dictionary<string, object>
                        {
                            { "IsDeleted", true }
                        };
                        newAction = "DELETED";
                        break;
                    case "DELETED":
                        dict = new Dictionary<string, object>
                        {
                            { "IsDeleted", false }
                        };
                        newAction = "ADDED";
                        break;
                    default:
                        dict = new Dictionary<string, object>();
                        newAction = string.Empty;
                        break;
                }
                batch.Update(itemRef, dict);

                DocumentReference historyRef = conn.FirestoreDb.Collection("item_history").Document(itemHistory.Id);
                batch.Delete(historyRef);

                string newId = GenerateId.GenerateCommon();
                DocumentReference newHistoryRef = conn.FirestoreDb.Collection("item_history").Document(newId);
                DocumentReference oldItemRef = conn.FirestoreDb.Collection("items").Document(itemHistory.ItemId);
                await conn.FirestoreDb.RunTransactionAsync(async transaction =>
                {
                    DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(oldItemRef);
                    Dictionary<string, object> itemDict = snapshot.ToDictionary();
                    Dictionary<string, object> historyDict = new Dictionary<string, object>
                    {
                        { "Id", newId },
                        { "StoreId", dataClass.Store.Id },
                        { "ItemId", itemDict["Id"].ToString()},
                        { "UserFullName", string.Format("{0} {1}", dataClass.LoggedInUser.FName, dataClass.LoggedInUser.LName) },
                        { "Barcode", itemDict["Barcode"].ToString() },
                        { "Name", itemDict["Name"].ToString() },
                        { "Brand", itemDict["Brand"].ToString() },
                        { "Price", Convert.ToDouble(itemDict["Price"]) },
                        { "Quantity", Convert.ToInt32(itemDict["Quantity"]) },
                        { "Action", newAction},
                        { "CommittedDate", DateTime.Now.ToUniversalTime() },
                    };
                    batch.Set(newHistoryRef, historyDict);
                });

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
