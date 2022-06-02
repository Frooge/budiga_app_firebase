using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.Core
{
    public class DataClass
    {
        private static DataClass _instance;
        public UserModel LoggedInUser { get; set; }

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

        
    }
}
