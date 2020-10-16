using System;
using System.IO;
using mantis_test;
using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class AccountTests : BaseTests
    {
        [Test]
        public void TestAPI()
        {
            AccountData acc = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            IssData iss = new IssData()
            {
                Descr = "Piper New Description",
                Summary = "Piper New Summuryyyyyyy",
                Catgr = "General"
            };

            ProjData pj = new ProjData()
            {
                ID = "1"
            };

            app.APIHelper.CreateNewIssue(acc, iss, pj);
        }

        [Test]
        public void TestSimpleBrowserLogin()
        {
            string ss = "3";
            AccountData acc = new AccountData()
            {
                Name = "testuser" + ss,
                Password = "password" + ss,
                Email = "testuser" + ss + "@localhost.localdomain"
            };

            app.AdmHelper.DeleteAccount(acc);
        }

        [Test]
        public void TestJamesHelper()
        {
            AccountData acc = new AccountData()
            {
                Name = "blobby",
                Password = "qwerty"
            };

            Assert.IsFalse(app.JHelper.Exists(acc));
            app.JHelper.Add(acc);
            Assert.IsTrue(app.JHelper.Exists(acc));
            app.JHelper.Delete(acc);
            Assert.IsFalse(app.JHelper.Exists(acc));
        }

        [Test]
        public void TestAccountCreation()
        {
            string ss = "4";
            AccountData acc = new AccountData()
            { 
                Name = "testuser" + ss,
                Password = "password" + ss,
                Email = "testuser" + ss + "@localhost.localdomain"
            };

            app.JHelper.Delete(acc);
            app.JHelper.Add(acc);

            app.RegHelper.Rigester(acc);
        }

        [SetUp]
        public void SetupConfig()
        {
            app.FTPHelper.BackupFile(BaseData.MantisConfigFileByFTP);
            using (Stream localFile = File.Open(@"C:\Users\user\Source\Repos\csharp_training\mantis-test\mantis-test\bin\Debug\" + BaseData.MantisConfigFileLocal, FileMode.Open))
                app.FTPHelper.Upload(BaseData.MantisConfigFileByFTP, localFile);
        }

        [TearDown]
        public void RestoreConfig()
        {
            app.FTPHelper.RestoreBackupFile(BaseData.MantisConfigFileByFTP);
        }

    }
}
