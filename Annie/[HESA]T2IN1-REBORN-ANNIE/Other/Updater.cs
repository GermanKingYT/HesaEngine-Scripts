using System.IO;
using System.Net;
using System.Xml;

namespace _HESA_T2IN1_REBORN_ANNIE.Other
{
    internal class Updater
    {
        private static string DownloadLink = string.Empty;
        private static string CachedXML = string.Empty;

        public static string Run(string Name, string Version)
        {
            if (!HasInternet())
            {
                return "Failed";
            }

            CacheXML("https://raw.githubusercontent.com/LeagueRaINi/HesaEngine-Scripts/master/Versions.xml");

            if (CachedXML.Equals(string.Empty))
            {
                return "Failed";
            }

            if (!NeedsUpdate(Name, Version))
            {
                return "NoUpdate";
            }

            return "NewVersion";
        }

        private static void CacheXML(string Link)
        {
            using (WebClient _WebClient = new WebClient())
            {
                CachedXML = _WebClient.DownloadString(Link);
            }
        }

        private static bool NeedsUpdate(string Name, string Version)
        {
            XmlDocument _Document = new XmlDocument();
            _Document.LoadXml(CachedXML);

            XmlNode _Names = _Document.DocumentElement.SelectSingleNode("/T2IN1_UPDATER/SCRIPTS");
            foreach (XmlNode _Node in _Names)
            {
                if (_Node.Attributes.GetNamedItem("VALUE").InnerText.Equals(Name))
                {
                    System.Version _CurrentVersion = new System.Version(Version); System.Version _DatabaseVersion = new System.Version(_Node.ChildNodes[0].InnerText);
                    if (_CurrentVersion.CompareTo(_DatabaseVersion) >= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool HasInternet()
        {
            try
            {
                using (WebClient _Client = new WebClient())
                {
                    using (Stream _Stream = _Client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
