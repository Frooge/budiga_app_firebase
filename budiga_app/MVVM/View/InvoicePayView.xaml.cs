using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoicePayView.xaml
    /// </summary>
    public partial class InvoicePayView : Window
    {
        private InvoiceModel _invoice;

        public InvoicePayView(InvoiceModel invoice)
        {
            InitializeComponent();
            _invoice = invoice;
        }
    }
}
