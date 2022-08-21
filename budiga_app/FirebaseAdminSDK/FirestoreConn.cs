using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace budiga_app
{
    public class FirestoreConn
    {
        private readonly string debugPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(
        Assembly.GetExecutingAssembly().Location), "..", "..", "FirebaseAdminSDK", "budiga-sea-firebase-adminsdk-o7omb-cfda0c0782.json"));
        private readonly string releasePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(
        Assembly.GetExecutingAssembly().Location), "..", "FirebaseAdminSDK", "budiga-sea-firebase-adminsdk-o7omb-cfda0c0782.json"));
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
                var jsonString = (File.Exists(debugPath)) ? File.ReadAllText(debugPath) : File.ReadAllText(releasePath);
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
                if (_instance == null)
                {
                    _instance = new FirestoreConn();
                }
                return _instance;
            }
        }


    }
}
