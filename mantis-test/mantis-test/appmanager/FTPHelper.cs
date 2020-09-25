using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace mantis_test
{
    public class FTPHelper : MBaseHelper
    {
        public FTPHelper(IWebDriver driver) : base(driver)
        {
        }
        public FTPHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
        }

        public void BackupFile(string path)
        {

        }

        public void RestoreBackupFile(string path)
        {

        }

        public void Upload(string path)
        {

        }
    }
}
