using System;
using System.Net;
using System.Xml;

using _HESA_T2IN1_REBORN_ANNIE;
using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN
{
    public class Program : IScript
    {
        public string Name => "[T2IN1-REBORN] Annie";
        public string Version => "1.2.0";
        public string Author => "RaINi";

        private static string _CachedXML = string.Empty;

        public void OnInitialize()
        {
            Game.OnGameLoaded += Game_OnGameLoaded;
        }

        private static void LoadScript()
        {
            Console.Clear();

            try
            {
                SpellsManager.Initialize();
                Menus.Initialize();
                Drawings.Initialize();
                DamageIndicator.Initialize();
                ModeManager.Initialize();
                /* Interrupt.Initialize(); TODO: FINISH */
            }
            catch (Exception _Exception)
            {
                Logger.Log("Error: " + _Exception, ConsoleColor.Red);
            }

            Chat.Print("<font color='#27ae60'>[T2IN1-REBORN] </font>Script is ready to use");
        }

        private void Game_OnGameLoaded()
        {
            if (Globals.MyHero.Hero.Equals(Champion.Annie))
            {
                if (_HasInternet())
                {
                    _CacheXML("https://raw.githubusercontent.com/LeagueRaINi/HesaEngine-Scripts/master/Versions.xml");
                    if (_CachedXML != string.Empty)
                    {
                        Chat.Print(_NeedsUpdate(Name, Version)
                            ? "<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>A New Update is available!"
                            : "<font color='#27ae60'>[T2IN1-UPDATE-CHECKER] </font>No Update found");
                    }
                }
                else
                {
                    Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>Could not check for Updates");
                }

                LoadScript();
            }
            else
            {
                Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN] </font> Champion: " + ObjectManager.Player.ChampionName + " is not Supported!");
            }
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
                    if (Version.Equals(_Node.ChildNodes[0].InnerText))
                    {
                        return false;
                    }
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
