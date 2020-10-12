using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.FtpClient;

using OpenQA.Selenium;

namespace mantis_test
{
    public class FTPHelper : MBaseHelper
    {
        private FtpClient client;
        public FTPHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
            client = new FtpClient();
            client.Host = "localhost";
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }

        public void BackupFile(string path)
        {
            String backPath = path + ".back";
            if (client.FileExists(backPath))
                return;
            client.Rename(path, backPath);
        }

        public void RestoreBackupFile(string path)
        {
            String backPath = path + ".back";
            if (!client.FileExists(backPath))
                return;
            if (client.FileExists(path))
                client.DeleteFile(path);
            client.Rename(backPath, path);
        }

        public void Upload(string path, Stream localFile)
        {
            if (client.FileExists(path))
                client.DeleteFile(path);

            using (Stream ftpStream = client.OpenWrite(path))
            {
                byte[] buff = new byte[8 * 1024];
                int c = localFile.Read(buff, 0, buff.Length);
                while(c > 0)
                {
                    ftpStream.Write(buff, 0, c);
                    c = localFile.Read(buff, 0, buff.Length);
                }
            }
        }
    }
}
