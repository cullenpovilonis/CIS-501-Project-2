using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    class Tenant
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

        public void unbarNumber(string number){
            foreach(string s in _barredNumbers){
                if (s == number){
                    _barredNumbers.Remove(s);
                }
            }
        }

        public void unbarAreaCode(string number){
            foreach(string s in _barredAreaCodes){
                if (s == number){
                    _barredAreaCodes.Remove(s);
                }
            }
        }

        public void addBarredNumber(string number){
            if ((!_barredNumbers.Contains(number)) && (number.Length == 10))
            {
                _barredNumbers.Add(number);
                return;
            }
            else return;
        }


        public void addBarredAreaCode(string number){
            if ((!_barredAreaCodes.Contains(number)) && (number.Length == 3))
            {
                _barredAreaCodes.Add(number);
                return;
            }
            else return;
        }


        bool checkNumbers(List<string> barred, string call){
            foreach(string s in barred){
                if (s == call)
                {
                    return false;
                }
                else return true;
            }
            return true;
        }
    }
}
