using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for InvoiceReceiptView.xaml
    /// </summary>
    public partial class InvoiceReceiptView : Window
    {
        public InvoiceModel Invoice { get; set; }
        public InvoiceReceiptView(InvoiceModel invoice)
        {
            Invoice = invoice;
            InitializeComponent();            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                PrintTicket pt = printDialog.PrintTicket;
                Double printableWidth = pt.PageMediaSize.Width.Value;
                Double printableHeight = pt.PageMediaSize.Height.Value;
                Double xScale = (printableWidth - 0 * 2) / printableWidth;
                Double yScale = (printableHeight - 0 * 2) / printableHeight;

                print.LayoutTransform = new MatrixTransform(xScale, 0, 0, yScale, 0, 0);
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "Invoice");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
