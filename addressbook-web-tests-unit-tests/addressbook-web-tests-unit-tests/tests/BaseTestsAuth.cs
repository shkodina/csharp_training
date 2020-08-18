using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class BaseTestsAuth : BaseTests
    {
        [SetUp]
        public void SetupTestWithLogon()
        {
            app.mLoginHelper.Login(new LoginData());
        }
    }
}
