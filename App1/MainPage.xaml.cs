using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.SerialCommunication;
using System.Text;
using Windows.Storage.Streams;
using Windows.Devices.Enumeration;
using System.Collections.ObjectModel;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;
using Windows.UI.Core;
using Windows.Devices.I2c;
using System.Threading.Tasks;







// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        SensorHelper sh = new SensorHelper();

        public MainPage()
        {
            this.InitializeComponent();
            constructor();
           // init();
        }

        void constructor()
        {
            sh.InitI2C();
            this.timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();

            Unloaded += MainPage_Unload;
        }

        private void MainPage_Unload(object sender, RoutedEventArgs e)
        {
            sh.dispose();
        }

        private void Timer_Tick(object sender, object e)
        {
            int distance = sh.getADC(0);
            textBox.Text = Convert.ToString(distance);
            if (distance>120)
            {
               // writeSerial("tag.read_id(2)");
            }
        }


    

        private void ScrolltoBottom(TextBox textBox)
        {
            var grid = (Grid)VisualTreeHelper.GetChild(textBox, 0);
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(grid); i++)
            {
                object obj = VisualTreeHelper.GetChild(grid, i);
                if (!(obj is ScrollViewer)) continue;
                //((ScrollViewer)obj).ChangeView(0.0f, ((ScrollViewer)obj).ExtentHeight, 1.0f);
                ((ScrollViewer)obj).ScrollToVerticalOffset(((ScrollViewer)obj).ExtentHeight);
                break;
            }
        }

      
    }
}
