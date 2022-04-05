using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;
using ZXing.Common;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for ScannerView.xaml
    /// </summary>
    public partial class ScannerView : UserControl
    {
        public ScannerView()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void ScannerControl_Loaded(object sender, RoutedEventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
            {
                deviceCmbBox.Items.Add(device.Name);
            }
            deviceCmbBox.SelectedIndex = 0;

            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;
        }

        private void window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.SignalToStop();
                    videoCaptureDevice.NewFrame -= new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
                    videoCaptureDevice = null;
                }
            }
        }

        private void scanBtn_Checked(object sender, RoutedEventArgs e)
        {
            scanBtn.Content = "Stop Scanning";
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[deviceCmbBox.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
            videoCaptureDevice.Start();
        }

        private void scanBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    scanBtn.Content = "Start Scanning";
                    videoCaptureDevice.SignalToStop();
                    videoCaptureDevice.NewFrame -= new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
                    videoCaptureDevice = null;
                }
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }  

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
            {
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    //PureBarcode = true,
                    PossibleFormats = new List<BarcodeFormat>
                    {
                        BarcodeFormat.EAN_13
                        //BarcodeFormat.CODE_128
                        //BarcodeFormat.EAN_8,
                        //BarcodeFormat.CODE_39,
                        //BarcodeFormat.UPC_A
                    }
                }
            };
            var result = reader.Decode(img);
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (result != null)
                {
                    outputBlock.Text = "Result: " + result.Text;
                }
                else
                {
                    outputBlock.Text = "No item scanned!";
                }
                scannerCamera.Source = ImageSourceFromBitmap(img);
            }));
        }

    }
}
