using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class LoginTests : BaseTests
    {
        [Test]
        public void CorrectLogin()
        {
            LoginData correctLogin = new LoginData("admin", "secret");

            app.mLoginHelper
                .Logout()
                .Login(correctLogin);
            Assert.IsTrue(app.mLoginHelper.IsLogin(correctLogin));
        }

        [Test]
        public void InCorrectLogin()
        {
            LoginData inCorrectLogin = new LoginData("admin", "admin");

            app.mLoginHelper
                .Logout()
                .Login(inCorrectLogin);
            Assert.IsFalse(app.mLoginHelper.IsLogin(inCorrectLogin));
        }
    }
}
