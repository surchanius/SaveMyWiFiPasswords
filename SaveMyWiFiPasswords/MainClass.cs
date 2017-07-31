using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaveMyWiFiPasswords
{
    class MainClass
    {
        static void Main(string[] args)
        {
            var saveWifi = new SaveWiFi();
            saveWifi.SaveAllPasswords();
        }
    }
}
