using budiga_app.MVVM.ViewModel;
using System.Windows;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoiceHistoryView.xaml
    /// </summary>
    public partial class InvoiceHistoryView : Window
    {
        public InvoiceViewModel ViewModel { get; set; }
        public InvoiceHistoryView()
        {
            ViewModel = InvoiceViewModel.GetInstance;
            InitializeComponent();
        }
    }
}
