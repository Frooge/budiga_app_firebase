using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.Core;
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
using budiga_app.MVVM.ViewModel;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoicePayView.xaml
    /// </summary>
    public partial class InvoicePayView : Window
    {
        public InvoiceViewModel ViewModel { get; set; }
        public InvoicePayView()
        {
            ViewModel = InvoiceViewModel.GetInstance;
            ViewModel.ClosePayView = new Action(this.Close);
            InitializeComponent();            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
