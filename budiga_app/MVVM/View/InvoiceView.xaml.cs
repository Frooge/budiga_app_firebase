using budiga_app.MVVM.ViewModel;
using System.Windows.Controls;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {
        public InvoiceViewModel ViewModel { get; set; }
        public InvoiceView()
        {
            ViewModel = InvoiceViewModel.GetInstance;
            InitializeComponent();
        }
    }
}
