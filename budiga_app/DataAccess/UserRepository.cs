using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using budiga_app.Core;
using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;

namespace budiga_app.DataAccess
{
    class UserRepository
    {
        private FirestoreConn conn = FirestoreConn.GetInstance;


        public UserRepository()
        {
            
        }

        public async Task<bool> AddEmployeeUser(UserModel user)
        {
            bool result = false;
            try
            {
                string newId = GenerateId.GenerateCommon();
                DocumentReference docRef = conn.FirestoreDb.Collection("users").Document(newId);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", newId },
                    { "StoreId", user.StoreId },
                    { "FName", user.FName },
                    { "LName", user.LName },
                    { "Username", user.Username },
                    { "Password", user.Password },
                    { "Contact", user.Contact },
                    { "Type", user.Type },
                    { "Online", false },
                    { "CreatedDate", DateTime.UtcNow },
                    { "IsDeleted", false }
                };
                await docRef.SetAsync(dict);
                result = true;
                MessageBox.Show("Successfully added user!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task<bool> UpdateUser(UserModel user)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("users").Document(user.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "StoreId", user.StoreId },
                    { "FName", user.FName },
                    { "LName", user.LName },
                    { "Username", user.Username },
                    { "Password", user.Password },
                    { "Contact", user.Contact },
                    { "CreatedDate", DateTime.UtcNow }
                };
                await docRef.UpdateAsync(dict);
                result = true;
                MessageBox.Show("Successfully updated user!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task<bool> DeleteUser(string id)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("users").Document(id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "IsDeleted", true },
                };
                await docRef.UpdateAsync(dict);
                result = true;
                MessageBox.Show("Successfully deleted user!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }
    }

    
}
