using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml;

namespace App1
{
    class SensorHelper
    {
        private const byte PDCF8591 = (0x90 >> 1);
        public const byte PDCF8591_ADC_CH0 = 0x40;
        public const byte PDCF8591_ADC_CH1 = 0x41;
        public const byte PDCF8591_ADC_CH2 = 0x42;
        public const byte PDCF8591_ADC_CH3 = 0x43;

        private I2cDevice I2CPCF8591;

        const long referenceMv = 5000;
        const int TABLE_ENTRIES = 12;
        const int NBR_ELEMS = 10;
        const int firstElement = 250;

        const int INTERVAL = 250;
        static int[] distance = { 150, 140, 130, 100, 60, 50, 40, 35, 30, 25, 20, 15 };


        public async void InitI2C()
        {
            string acs = I2cDevice.GetDeviceSelector();
            var dls = await DeviceInformation.FindAllAsync(acs);

            if (dls.Count() == 0)
            {
                return;
            }

            var settings = new I2cConnectionSettings(PDCF8591);
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            I2CPCF8591 = await I2cDevice.FromIdAsync(dls[0].Id, settings);

            if (I2CPCF8591 == null)
            {
                return;
            }

        }

        public int getADC(int ch)
        {
            byte[] recv = new byte[2];

            if (ch ==0)
            I2CPCF8591.WriteRead(new byte[] { PDCF8591_ADC_CH0 }, recv);
            else if (ch == 0)
                I2CPCF8591.WriteRead(new byte[] { PDCF8591_ADC_CH1 }, recv);
            else if(ch == 0)
                I2CPCF8591.WriteRead(new byte[] { PDCF8591_ADC_CH2 }, recv);
            else if(ch == 0)
                I2CPCF8591.WriteRead(new byte[] { PDCF8591_ADC_CH3 }, recv);

            return recv[1];
        }

        public void dispose()
        {
            I2CPCF8591.Dispose();
        }


    }
}
