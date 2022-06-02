using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class LoginViewModel
    {
        public SystemModel System { get; set; }

        public LoginViewModel()
        {
            System = new SystemModel();
        }
    }
}
