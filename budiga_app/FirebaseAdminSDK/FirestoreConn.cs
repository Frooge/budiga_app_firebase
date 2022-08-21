using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace budiga_app
{
    public class FirestoreConn
    {
        private readonly string path = "..\\..\\FirebaseAdminSDK\\" + @"budiga-sea-firebase-adminsdk-o7omb-cfda0c0782.json";
        private static FirestoreConn _instance;
        public FirestoreDb FirestoreDb { get; set; }

        public FirestoreConn()
        {
            Connect();
        }

        private void Connect()
        {
            try
            {
                var jsonString = File.ReadAllText(path);
                var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
                FirestoreDb = FirestoreDb.Create("budiga-sea", builder.Build()); // Database name
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static FirestoreConn GetInstance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new FirestoreConn();
                }
                return _instance;
            }
        }

        
    }
}
