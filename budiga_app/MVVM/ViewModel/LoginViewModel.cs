using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class LoginViewModel
    {
        public PageModel Page { get; set; }

        public LoginViewModel()
        {
            Page = new PageModel();
        }
    }
}
