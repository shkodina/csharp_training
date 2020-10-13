using System;
using System.IO;
using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class AccountTests : BaseTests
    {
        [Test]
        public void xxx()
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
            AccountData acc = new AccountData()
            { 
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
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
