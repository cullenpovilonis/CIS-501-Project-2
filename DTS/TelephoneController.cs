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
        public Tenant _tenant;
        public List<Tenant> _tenants;
        public TerminalController _term;


        public void Set(TerminalController t) => _term = t;


        public TelephoneController(ITelephoneDevice telephoneDevice)
        {
            this.telephoneDevice = telephoneDevice;
        }


        public void Activate()
        {
            // Receive an access code
            _tenants = _term._tenants;
            string accessCode = null;
            if (!telephoneDevice.GetAccessCode(ref accessCode)) return;
            foreach (Tenant t in _tenants)
            {
                if (t._accessCode.Equals(accessCode))
                {
                    _tenant = t;
                    // Recieve a telephone number
                    string areaCode = null;
                    string exchange = null;
                    string number = null;
                    if (!telephoneDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
                    if (_tenant.checkBar(areaCode, exchange, number) == false)
                    {
                        DateTime beginCall = DateTime.Now;
                        // Connect the phone
                        telephoneDevice.ConnectPhone();
                        // User has terminated the call
                        DateTime endCall = DateTime.Now;
                        Calls call = new Calls(areaCode, exchange, number, beginCall, endCall);
                        _tenant.addCall(call);
                    }
                    else return;
                }
            }
        }
    }
}
