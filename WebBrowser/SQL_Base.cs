using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebBrowser
{
    public class SQL_Base
    {
        protected readonly string accConnectionString;

        public SQL_Base()
        {
            string settingFile = this.GetSettingFile();
            ConnectionSetting setting = new ConnectionSetting(settingFile);
            this.accConnectionString = setting.GetAccount();
        }

        private string GetSettingFile()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(filePath, "Connections.config");
        }
    }

    public class ConnectionSetting
    {
        private string settingFile;

        public ConnectionSetting(string settingFile)
        {
            this.settingFile = settingFile;
        }

        public string GetMaster()
        {
            return GetValueByName("master");
        }

        public string GetAccount()
        {
            return GetValueByName("account");
        }

        private string GetValueByName(string p)
        {
            XmlDocument document = new XmlDocument();
            document.Load(settingFile);
            string path = string.Format("/connectionStrings/add[@name='{0}']/@connectionString", p);
            return document.SelectSingleNode(path).InnerText;
        }
    }
}
