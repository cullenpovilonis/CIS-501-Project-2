using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS_Project
{
    public class TelephoneController
    {

        private ITelephoneDevice telephoneDevice;
        // You need to add reference and/or value fields of TelephoneController
        // You may need to add Set methods to set the initlize values of these fields
        // These set methods are called from DTSInitializer.Initialize()
        
        public TelephoneController(ITelephoneDevice telephoneDevice)
        {
            this.telephoneDevice = telephoneDevice;
        }

        public void Activate()
        {
            // Receive an access code
            string accessCode = null;
            if (!telephoneDevice.GetAccessCode(ref accessCode)) return;


            // Recieve a telephone number
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!telephoneDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;


            // Connect the phone
            telephoneDevice.ConnectPhone();
            // User has terminated the call

        }
    }
}
