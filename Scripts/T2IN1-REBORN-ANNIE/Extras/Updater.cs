using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

using HesaEngine.SDK;

namespace T2IN1_REBORN_ANNIE.Extras
{
    internal class Updater
    {
        private static string CachedXML = string.Empty;

        public static string Run(string name, string version)
        {
            Logger.Log(">> Executed");

            if (!HasInternet()) return "Failed";

            CacheXML("https://raw.githubusercontent.com/LeagueRaINi/HesaEngine-Scripts/master/Versions.xml");

            if (CachedXML.Equals(string.Empty)) return "Failed";

            return !NeedsUpdate(name, version) ? "NoUpdate" : "NewVersion";
        }

        private static bool HasInternet()
        {
            try 
            {
                using (WebClient client = new WebClient()) 
                {
                    using (Stream stream = client.OpenRead("http://www.google.com")) 
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

        private static void CacheXML(string link)
        {
            using (WebClient webClient = new WebClient()) 
            {
                CachedXML = webClient.DownloadString(link);
            }
        }

        private static bool NeedsUpdate(string name, string version)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(CachedXML);

            XmlNode nodes = document.DocumentElement.SelectSingleNode("/T2IN1_UPDATER/SCRIPTS");
            return nodes.Cast<XmlNode>().Where(node => node.Attributes.GetNamedItem("VALUE").InnerText.Equals(name)).All(node => new Version(version).CompareTo(new Version(node.ChildNodes[0].InnerText)) < 0);
        }
    }
}
