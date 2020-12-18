using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIA;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using System.Threading;
using System.Drawing.Imaging;
using Vector = WIA.Vector;

namespace UP_Scanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DeviceManager dm = null;
        ImageFile scannedFile = null;
        int colorSettting = 1;
        int dpi = 100;
        public ObservableCollection<String> DeviceNames { get; set; }
        public List<DeviceInfo> Devices { get; set; }
        public String ChosenScannerName
        {
            get { return _ChosenScannerName; }
            set
            {
                _ChosenScannerName = value;
                this.OnPropertyChanged("ChosenScanner");
                foreach (DeviceInfo deviceInfo in Devices)
                {
                    if (deviceInfo.Properties["Name"].get_Value().Equals(_ChosenScannerName))
                    {
                        ChosenScanner = deviceInfo;
                    }
                }
            }
        }
        private String _ChosenScannerName;

        public DeviceInfo ChosenScanner
        {
            get { return _ChosenScanner; }
            set
            {
                _ChosenScanner = value;
                this.OnPropertyChanged("ChosenScanner");
            }
        }
        private DeviceInfo _ChosenScanner;

        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = this;
            getDevices();
        }
        
        private void ScanTypeSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            
            switch (ScanType.SelectedIndex)
            {
                case 0:
                    colorSettting = 2;
                    break;
                case 1:
                    colorSettting = 4;
                    break;
                case 2:
                    colorSettting = 1;
                    break;
                default:
                    colorSettting = 2;
                    break;
            }
        }

        private void ResolutionSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (Resolution.SelectedIndex)
            {
                case 0:
                    dpi = 100;
                    break;
                case 1:
                    dpi = 200;
                    break;
                case 2:
                    dpi = 300;
                    break;
                case 3:
                    dpi = 400;
                    break;
                case 4:
                    dpi = 500;
                    break;
                case 5:
                    dpi = 600;
                    break;
                default:
                    dpi = 100;
                    break;
            }
        }

        private void getDevices() {
            dm = new DeviceManager();
            DeviceNames = new ObservableCollection<String>();
            Devices = new List<DeviceInfo>();
            // Loop through the list of devices to choose the first available
            for (int i = 1; i <= dm.DeviceInfos.Count; i++)
            {
                // Skip the device if it's not a scanner
                if (dm.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                DeviceNames.Add((String)dm.DeviceInfos[i].Properties["Name"].get_Value());
                Devices.Add(dm.DeviceInfos[i]);

            }
            if (Devices.Any())
            {
                ChosenScannerName = DeviceNames[0];

                foreach (DeviceInfo deviceInfo in Devices) {
                    if (deviceInfo.Properties["Name"].get_Value().Equals(_ChosenScannerName))
                    {
                        ChosenScanner = deviceInfo;
                    }
                }
            }
            else
            {
                MessageBox.Show("No scanners were found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel, int brightnessPercents, int contrastPercents, int colorMode)
        {
            const string WIA_SCAN_COLOR_MODE = "6146";
            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);
        }

        static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }

        private void ClickSearch(object sender, RoutedEventArgs e)
        {
            getDevices();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "image files (*.jpeg)|*.jpeg|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "scan.jpeg";
            var dialogresult = saveFileDialog.ShowDialog();
            if (dialogresult != true)
            {
                return;
            }

            if (File.Exists(saveFileDialog.FileName))
                    File.Delete(saveFileDialog.FileName);

            scannedFile.SaveFile(saveFileDialog.FileName);
        }

        

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            var selectedDevice = ChosenScanner.Connect();
            var scanner = selectedDevice.Items[1];
            AdjustScannerSettings(scanner, dpi, 0, 0, 0, 0, colorSettting);
            scannedFile = (ImageFile)scanner.Transfer(WIA.FormatID.wiaFormatJPEG);
            var imageBytes = (byte[])scannedFile.FileData.get_BinaryData();
            BitmapImage bitmapImage = ToImage(imageBytes);
            Dispatcher.BeginInvoke(new ThreadStart(delegate { mainImage.Source = bitmapImage; }));

        }

        private void ScanDriver_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WIA.CommonDialog();
            ImageFile image = dlg.ShowAcquireImage(
                DeviceType: WiaDeviceType.ScannerDeviceType,
                Intent: WiaImageIntent.ColorIntent,
                Bias: WiaImageBias.MinimizeSize,
                FormatID: ImageFormat.Jpeg.Guid.ToString("B"),
                AlwaysSelectDevice: true,
                UseCommonUI: true,
                CancelError: false);
            Vector vector = image.FileData;
            if (vector != null)
            {
                byte[] imageBytes = vector.get_BinaryData() as byte[];
                BitmapImage bitmapImage = ToImage(imageBytes);
                Dispatcher.BeginInvoke(new ThreadStart(delegate { mainImage.Source = bitmapImage; }));
            }
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
    
}


