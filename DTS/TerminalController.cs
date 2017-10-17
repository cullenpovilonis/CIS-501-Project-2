using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DTS_Project
{

    public class TerminalController
    {
        private ITerminalDevice terminalDevice;
        public List<Tenant> _tenants = new List<Tenant>();
        private Tenant _tenant;
        private SystemAdmin admin = new SystemAdmin();

        public void SetTenants(List<Tenant> t) => _tenants = t;

        public TerminalController(ITerminalDevice terminalDevice)
        {
            this.terminalDevice = terminalDevice;
        }

        public void Activate()
        {
            //verify password and if verified, show MainMenuDialog
            // if a user presses "Cancel", do nothing and just return
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            // you need to verify the password
            if (admin.verifyPassword(password) == true) terminalDevice.ShowMainMenuDialog(this);
        }

        // handlers for MainMenuDialog
        public void AddTenant_Handler()
        {
            // Add a tenant
            // Get the name and access code of the tenant to be added
            string firstName = null;
            string lastName = null;
            string accessCode = null;
            if (!terminalDevice.GetTenantInfo(ref firstName, ref lastName, ref accessCode)) return;
            Tenant tenant = new Tenant(firstName, lastName, accessCode);
            _tenants.Add(tenant);

        }

        public void DeleteTenant_Handler()
        {
            // Delete a tenant
            // Get the first name and the last name of the tenant to be deleted
            string firstName = null;
            string lastName = null;
            if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;
            foreach (Tenant t in _tenants.ToArray()){
                if (t._firstName.Equals(firstName)){
                    if (t._lastName.Equals(lastName)){
                        _tenants.Remove(t);
                    }
                }
            }
        }

        public void WorkOnTenant_Handler()
        {
            // Work on a specific tenant
            // Input the first name and the last name of the tenant to work on
            string firstName = null;
            string lastName = null;
            if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;
            foreach (Tenant t in _tenants)
            {
                if (t._firstName.Equals(firstName))
                {
                    if (t._lastName.Equals(lastName))
                    {
                        _tenant = t;
                        terminalDevice.ShowTenantMenuDialog(this);
                    }
                }
            }

        }

        public void DisplayTenantList_Handler()
        {
            List<string> tenants = new List<string>();
            foreach (Tenant t in _tenants){
                tenants.Add(t.ToString());
            }
            terminalDevice.DisplayList(tenants.ToArray());
            // call "void DisplayList(object[] list)" to list Tenants
        }

        public void Save_Handler()
        {
            BinaryFormatter fo = new BinaryFormatter();
            using (FileStream f = new FileStream("DTSsavefile.svf",
                FileMode.Create, FileAccess.Write))
            {
                fo.Serialize(f, _tenants);
            }
        }

        public void Restore_Handler()
        {
            BinaryFormatter fo = new BinaryFormatter();
            using (FileStream f = new FileStream("DTSsavefile.svf",
                FileMode.Open, FileAccess.Read))
            {
                _tenants = (List<Tenant>)fo.Deserialize(f);
            }
        }

        public void ChangePassword_Handler()
        {
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            admin.changePassword(password);

        }

        // ==== Handlers for TenantMenuDialog
        public void BarAreaCode_Handler()
        {
            // Bar an area code
            // Input the area code to bar
            string areaCode = null;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            _tenant.addBarredAreaCode(areaCode);

        }

        public void BarTelephoneNumber_Handler()
        {
            // Bar a telephone number
            // Input the telephone number to bar
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            string num = (areaCode + exchange + number).ToString();
            _tenant.addBarredNumber(num);

        }

        public void UnBarAreaCode_Handler()
        {
            // Unbar an area code
            // Input the area code to unbar
            string areaCode = null;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            _tenant.unbarAreaCode(areaCode);

        }

        public void UnBarTelephoneNumber_Handler()
        {
            // Unbar a telephone number
            // Input the telephone number to unbar 
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            string num = (areaCode + exchange + number).ToString();
            _tenant.unbarNumber(num);

        }

        public void DisplayCallList_Handler()
        {
            List<string> temp = new List<string>();
            foreach (Calls s in _tenant._calls){
                string call = s.formatCall();
                temp.Add(call);
            }

            terminalDevice.DisplayList(temp.ToArray());
            // call  "void DisplayList(object[] list)" to list Calls
        }

        public void DisplayBarList_Handler()
        {
            List<string> barred = new List<string>();
            foreach(string s in _tenant._barredNumbers)
            {
                barred.Add(s);
            }
            foreach(string s in _tenant._barredAreaCodes)
            {
                barred.Add(s);
            }
            terminalDevice.DisplayList(barred.ToArray());
            // call "void DisplayList(object[] list)" to list Bar Numbers

        }

        public void ClearCalls_Handler()
        {
            _tenant._calls = new List<Calls>();
        }
    }
}
