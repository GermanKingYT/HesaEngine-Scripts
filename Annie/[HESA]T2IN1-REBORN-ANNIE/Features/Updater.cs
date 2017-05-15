using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml;

using HesaEngine.SDK;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal class Updater
    {
        private static string _DownloadLink = string.Empty;
        private static string _CachedXML = string.Empty;

        public static string Run(string Name, string Version)
        {
            if (!_HasInternet())
            {
                return "Failed";
            }

            _CacheXML("https://raw.githubusercontent.com/LeagueRaINi/HesaEngine-Scripts/master/Versions.xml");

            if (_CachedXML.Equals(string.Empty))
            {
                return "Failed";
            }

            if (!_NeedsUpdate(Name, Version))
            {
                return "NoUpdate";
            }

            string _Temp = _FoundScript();
            if (_Temp.Equals(string.Empty) || _DownloadLink.Equals(string.Empty))
            {
                return "NewVersion";
            }

            using (WebClient _WebClient = new WebClient())
            {
                Chat.Print("<font color='#27ae60'>[T2IN1-UPDATE-CHECKER] </font>Downloading the new Update");
                _WebClient.DownloadFile(new Uri(_DownloadLink), _Temp);
            }

            return new FileInfo(_Temp).Length > 0 ? "Updated" : "DownloadFailed";
        }



        private static string _FoundScript()
        {
            foreach (var _Script in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)), "Scripts"), "*.dll*", SearchOption.AllDirectories).ToList())
            {
                FileVersionInfo _FileInformation = FileVersionInfo.GetVersionInfo(_Script);
                if (_FileInformation.OriginalFilename.Equals("[HESA]T2IN1-REBORN-ANNIE.dll"))
                {
                    return _Script;
                }
            }
            return string.Empty;
        }

        private static void _CacheXML(string Link)
        {
            using (var _WebClient = new WebClient())
            {
                _CachedXML = _WebClient.DownloadString(Link);
            }
        }

        private static bool _NeedsUpdate(string Name, string Version)
        {
            var _Document = new XmlDocument();
            _Document.LoadXml(_CachedXML);

            var _Names = _Document.DocumentElement.SelectSingleNode("/T2IN1_UPDATER/SCRIPTS");
            foreach (XmlNode _Node in _Names)
            {
                if (_Node.Attributes.GetNamedItem("VALUE").InnerText.Equals(Name))
                {
                    var _CurrentVersion = new System.Version(Version); var _DatabaseVersion = new System.Version(_Node.ChildNodes[0].InnerText);
                    if (_CurrentVersion.CompareTo(_DatabaseVersion) >= 0)
                    {
                        return false;
                    }
                    _DownloadLink = _Node.ChildNodes[1].InnerText;
                }
            }
            return true;
        }

        public static bool _HasInternet()
        {
            try
            {
                using (var _Client = new WebClient())
                {
                    using (var _Stream = _Client.OpenRead("http://www.google.com"))
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
