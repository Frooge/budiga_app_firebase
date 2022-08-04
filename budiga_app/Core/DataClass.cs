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
        private AttendanceModel Attendance { get; set; }
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

        public static void ReleaseInstance()
        {
            _instance = null;
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
                        DocumentReference docRef = conn.FirestoreDb.Collection("Stores").Document(LoggedInUser.StoreId).Collection("Branch").Document(LoggedInUser.BranchId);
                        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                        if (snapshot.Exists)
                        {
                            Dictionary<string, object> dict = snapshot.ToDictionary();
                            Store = new StoreModel
                            {
                                Id = LoggedInUser.StoreId,
                                Branch = new StoreModel.BranchModel
                                {
                                    Id = dict["Id"].ToString(),
                                    Name = dict["Name"].ToString(),
                                    Location = dict["Location"].ToString()
                                }                                
                            };
                        }
                    }
                    else if (LoggedInUser.Type.Equals("admin"))
                    {
                        Query query = conn.FirestoreDb.Collection("Stores").Document(LoggedInUser.StoreId).Collection("Branch");
                        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                        Store = new StoreModel
                        {
                            Id = LoggedInUser.StoreId,
                            Branch = new StoreModel.BranchModel(),
                            BranchRecords = new ObservableCollection<StoreModel.BranchModel>()
                        };
                        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                        {
                            Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                            Store.BranchRecords.Add(new StoreModel.BranchModel
                            {
                                Id = dict["Id"].ToString(),
                                Name = dict["Name"].ToString(),
                                Location = dict["Location"].ToString()
                            });
                        }
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

        public async Task<bool> CheckIn()
        {
            bool result = false;
            if (LoggedInUser != null && Store != null && Attendance == null)
            {
                try
                {
                    FirestoreConn conn = FirestoreConn.GetInstance;
                    WriteBatch batch = conn.FirestoreDb.StartBatch();

                    DocumentReference userRef = conn.FirestoreDb.Collection("Users").Document(LoggedInUser.Id);
                    Dictionary<string, object> userDict = new Dictionary<string, object>
                    {
                        { "Online", true }
                    };
                    batch.Update(userRef, userDict);
                   
                    string newAttId = GenerateId.GenerateCommon();
                    Attendance = new AttendanceModel
                    {
                        Id = newAttId,
                        UserFullName = string.Format("{0} {1}", LoggedInUser.FName, LoggedInUser.LName),
                        TimeIn = DateTime.UtcNow,
                        TimeOut = null
                    };
                    DocumentReference attRef = conn.FirestoreDb.Collection("Stores").Document(Store.Id).Collection("Attendance").Document(Attendance.Id);
                    Dictionary<string, object> attDict = new Dictionary<string, object>
                    {
                        { "Id", Attendance.Id },
                        { "UserFullName", Attendance.UserFullName },
                        { "TimeIn", Attendance.TimeIn },
                        { "TimeOut", Attendance.TimeOut }
                    };
                    batch.Set(attRef, attDict);

                    await batch.CommitAsync();

                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }

        public async Task<bool> Checkout()
        {
            bool result = false;
            if (LoggedInUser != null && Store != null && Attendance != null)
            {
                try
                {
                    FirestoreConn conn = FirestoreConn.GetInstance;
                    WriteBatch batch = conn.FirestoreDb.StartBatch();

                    DocumentReference userRef = conn.FirestoreDb.Collection("Users").Document(LoggedInUser.Id);
                    Dictionary<string, object> userDict = new Dictionary<string, object>
                    {
                        { "Online", false }
                    };
                    batch.Update(userRef, userDict);

                    DocumentReference attRef = conn.FirestoreDb.Collection("Stores").Document(Store.Id).Collection("Attendance").Document(Attendance.Id);
                    Dictionary<string, object> attDict = new Dictionary<string, object>
                    {
                        { "TimeOut", DateTime.UtcNow }
                    };
                    batch.Update(attRef, attDict);

                    await batch.CommitAsync();

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
