using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.Core
{
    public static class Sessions
    {
        public static UserModel session { get; set; }

        public static void Dispose()
        {
            session = new UserModel();
        }
    }
}
