using HesaEngine.SDK;

namespace T2IN1_REBORN_AIO
{
    internal static class Globals
    {
        /* Menu */
        public static Menu RootMenu { get; set; }
        public static Menu ChampionMenu { get; set; }
        public static Menu PluginMenu { get; set; }

        /* Orbwalker */
        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;
        public static Orbwalker.OrbwalkingMode OrbMode => Orb.ActiveMode;
    }
}
