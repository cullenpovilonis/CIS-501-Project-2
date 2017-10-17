using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    [Serializable()]
    public class Calls
    {

        public string _areaCode { get; set; }
        public string _prefix { get; set; }
        public string _lineNumber { get; set; }
        DateTime startCall { get; }
        DateTime endCall { get; }

        public Calls(string areaCode, string prefix, string lineNumber, DateTime start, DateTime end){
            _areaCode = areaCode;
            _prefix = prefix;
            _lineNumber = lineNumber;
            startCall = start;
            endCall = end;
        }


        public string formatCall() => (_areaCode + "-" + _prefix + "-" + _lineNumber + "  Call Time: " +
        "" + startCall.ToLongTimeString() + " to " + endCall.ToLongTimeString() + " on " + endCall.ToShortDateString());


        public string ToString() => (_areaCode + _prefix + _lineNumber).ToString();
            
        }
    }

