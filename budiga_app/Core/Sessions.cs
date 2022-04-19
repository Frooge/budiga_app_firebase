using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.Core
{
    public static class Sessions
    {
        public static object[] session {get; set;}
        
        public static void dispose()
        {
            session = null;
        }
    }
}
