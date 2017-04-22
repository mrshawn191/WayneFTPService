using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace WayneFTPService
{
    public interface IFTPService
    {
        void DownloadAllFiles();

        string ListAllFiles();
    }

    public class FTPService : IFTPService
    {
        public void DownloadAllFiles()
        {
            Console.WriteLine("Starting...");
        }

        public string ListAllFiles()
        {
            FtpWebResponse response = (FtpWebResponse) GetConnection(WebRequestMethods.Ftp.ListDirectoryDetails)
                .GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            var result = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return result;
        }

        private FtpWebRequest GetConnection(string action)
        {
            var ftpConfig = GetConfigFromJson();

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create(ftpConfig.Host + ftpConfig.SpecifiedPath);
            request.Method = action;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(ftpConfig.Username, ftpConfig.Password);

            return request;
        }

        private FTPConfig GetConfigFromJson()
        {
            FTPConfig ftp = JsonConvert.DeserializeObject<FTPConfig>(File.ReadAllText("FTPConfig/config.json"));

            var ftpConfig = new FTPConfig
            {
                Host = ftp.Host,
                Username = ftp.Username,
                Password = ftp.Password,
                SpecifiedPath = ftp.SpecifiedPath
            };

            return ftpConfig;
        }
    }
}