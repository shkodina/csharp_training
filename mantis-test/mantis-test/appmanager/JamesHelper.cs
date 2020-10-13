using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using MinimalisticTelnet;

namespace mantis_test
{
    public class JamesHelper : MBaseHelper
    {
        public JamesHelper(IWebDriver driver, AppManager appManager) : base(driver, appManager)
        {
        }
        public void Add(AccountData ad)
        {
            if (Exists(ad))
                return;
            TelnetConnection telnet =  LoginToJames();
            telnet.WriteLine("adduser " + ad.Name + " " + ad.Password);
            System.Console.Out.WriteLine(telnet.Read());
        }


        public void Delete(AccountData ad)
        {
            if (!Exists(ad))
                return;
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + ad.Name);
            System.Console.Out.WriteLine(telnet.Read());
        }

        public bool Exists(AccountData ad)
        {
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + ad.Name);
            string s = telnet.Read();
            System.Console.Out.WriteLine(s);
            return !s.Contains("does not");
        }

        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection(BaseData.TelnetHost, BaseData.TelnetPort);
            System.Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            System.Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            System.Console.Out.WriteLine(telnet.Read());
            return telnet;
        }

    }
}
