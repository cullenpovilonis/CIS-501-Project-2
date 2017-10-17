using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    public class SystemAdmin
    {
        public string loginCode = "ksu";

        public void changePassword(string code) => loginCode = code;

        public bool verifyPassword(string code) => loginCode.Equals(code);

    }
}
