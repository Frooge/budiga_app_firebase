using budiga_app.MVVM.ViewModel;
using System;
using System.Windows;

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
