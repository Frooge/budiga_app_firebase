using budiga_app.Core;
using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.DataAccess
{
    class BranchRepository
    {
        private FirestoreConn conn;
        private DataClass dataClass;

        public BranchRepository()
        {
            conn = FirestoreConn.GetInstance;
            dataClass = DataClass.GetInstance;
        }

        public async Task<bool> AddBranch(StoreModel.BranchModel branch)
        {
            bool result = false;
            try
            {
                DocumentReference docRef = conn.FirestoreDb.Collection("Stores").Document(dataClass.Store.Id).Collection("Branch").Document(branch.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", branch.Id},
                    { "Name", branch.Name },
                    { "Location", branch.Location }
                };
                await docRef.SetAsync(dict);

                dataClass.Store.BranchRecords.Add(branch);
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
