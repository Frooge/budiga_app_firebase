using budiga_app.MVVM.Model;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.Core
{
    public class DataClass
    {
        private static DataClass _instance;
        public UserModel LoggedInUser { get; set; }
        public StoreModel Store { get; set; }
        public static DataClass GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataClass();
                }
                return _instance;
            }
        }

        public async Task<bool> SetStore()
        {
            bool result = false;
            if(Store == null && LoggedInUser != null)
            {
                try
                {
                    FirestoreConn conn = FirestoreConn.GetInstance;
                    if (LoggedInUser.Type.Equals("employee"))
                    {
                        DocumentReference docRef = conn.FirestoreDb.Collection("stores").Document(LoggedInUser.StoreId);
                        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                        if (snapshot.Exists)
                        {
                            Dictionary<string, object> dict = snapshot.ToDictionary();
                            Store = new StoreModel
                            {
                                Id = dict["Id"].ToString(),
                                Name = dict["Name"].ToString(),
                                Location = dict["Location"].ToString()
                            };
                        }
                    }
                    else if (LoggedInUser.Type.Equals("admin"))
                    {
                        Query query = conn.FirestoreDb.Collection("stores");
                        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                        Store = new StoreModel
                        {
                            StoreRecords = new ObservableCollection<StoreModel>()
                        };
                        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                        {
                            Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                            Store.StoreRecords.Add(new StoreModel
                            {
                                Id = dict["Id"].ToString(),
                                Name = dict["Name"].ToString(),
                                Location = dict["Location"].ToString()
                            });
                        }
                        Store.Id = Store.StoreRecords[0].Id;
                        Store.Name = Store.StoreRecords[0].Name;
                        Store.Location = Store.StoreRecords[0].Location;
                    }
                    else
                    {
                        Store = new StoreModel();
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }
    }
}
