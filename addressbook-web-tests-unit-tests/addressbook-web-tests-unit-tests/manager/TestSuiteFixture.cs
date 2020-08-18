using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [SetUpFixture]
    public class TestSuiteFixture
    {
        private static AppManager app;

        [OneTimeSetUpAttribute]
        public void InitManager()
        {
            AppManager.GetInstance().mLoginHelper.Login(new LoginData());
        }

        [OneTimeTearDownAttribute]
        public void StopManager()
        {

        }

        public AppManager GetApp()
        {
            return app;
        }
    }
}
