using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

using OpaqueMail;

namespace mantis_test
{
    public class MailHelper : MBaseHelper
    {
        public MailHelper(IWebDriver driver, AppManager appManager) : base(driver, appManager)
        {
        }

        public String GetLastMail(AccountData acc)
        {
            int waitInSeconds = 30;
            for (int i = 0; i < waitInSeconds; i++)
            {
                Pop3Client pop3 = new Pop3Client("localhost", 110, acc.Name, acc.Password, false);
                pop3.Connect();
                pop3.Authenticate();

                if (pop3.GetMessageCount() > 0)
                {
                    MailMessage m = pop3.GetMessage(1);
                    string str_body = m.Body;
                    pop3.DeleteMessage(1);
                    return str_body;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            return null;
        }
    }
}
