using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project

{   [Serializable()]
    public class Tenant
    {
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _accessCode { get; set; }
        public List<Calls> _calls = new List<Calls>();
        public List<string> _barredNumbers = new List<string>();
        public List<string> _barredAreaCodes = new List<string>();


        public Tenant(string first, string last, string code){
            _firstName = first;
            _lastName = last;
            _accessCode = code;
        }


        public void addCall(Calls call){
            _calls.Add(call);
        }


        public string ToString(){
            return (_firstName + " " + _lastName + ". Access Code: " + _accessCode);
        }


        public void unbarNumber(string number){
            foreach(string s in _barredNumbers.ToArray()){
                if (s == number){
                    _barredNumbers.Remove(s);
                }
            }
        }

        public void unbarAreaCode(string number){
            foreach(string s in _barredAreaCodes.ToArray()){
                if (s == number){
                    _barredAreaCodes.Remove(s);
                }
            }
        }

        public void addBarredNumber(string number){
            if (!_barredNumbers.Contains(number))
            {
                _barredNumbers.Add(number);
                return;
            }
            else return;
        }


        public void addBarredAreaCode(string number){
            if (!_barredAreaCodes.Contains(number))
            {
                _barredAreaCodes.Add(number);
                return;
            }
            else return;
        }


        public bool checkNumbers(List<string> barred, string call){
            foreach(string s in barred){
                if (s == call) return false;
                else return true;
            }
            return true;
        }

        public bool checkBar(string area, string prefix, string number){
            string bar = (area + prefix + number).ToString();
            if (_barredNumbers.Contains(bar) || _barredAreaCodes.Contains(area)) return true;
            else return false;

        }

    }
}
