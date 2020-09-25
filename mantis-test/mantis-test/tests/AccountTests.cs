using System;
using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class AccountTests : BaseTests
    {
        [Test]
        public void TestAccountCreation()
        {
            AccountData acc = new AccountData()
            { 
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            app.RegHelper.Rigester(acc);
        }
    }
}
