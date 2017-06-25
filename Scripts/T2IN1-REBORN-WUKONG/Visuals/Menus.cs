using System;

using HesaEngine.SDK;

namespace T2IN1_REBORN_WUKONG.Visuals
{
    internal class Menus
    {
        public static Menu HomeMenu;
        public static Menu ActivatorMenu;
        public static Menu ComboMenu;
        public static Menu LaneClearMenu;
        public static Menu LastHitMenu;
        public static Menu VisualsMenu;
        public static Menu MiscMenu;

        public static void Initialize()
        {
            HomeMenu = Menu.AddMenu("[T2IN1-REBORN] " + Globals.MyHero.ChampionName);

            /* Activator Section */
            ActivatorMenu = HomeMenu.AddSubMenu("> ACTIVATOR");
            ActivatorMenu.AddSeparator("-Activate-");
            ActivatorMenu.Add(new MenuCheckbox("EnableActivator", "Enable Activator", true));
            ActivatorMenu.AddSeparator("-Summoners-");
            ActivatorMenu.Add(new MenuCheckbox("AutoIgnite", "Auto use Ignite if Killable"));
            ActivatorMenu.AddSeparator("-Pots-");
            ActivatorMenu.Add(new MenuCheckbox("AutoUsePots", "Auto use Health Pots", true));
            ActivatorMenu.Add(new MenuCheckbox("AutoUsePotsOnBuff", "Auto use Health Pots if Poisened / has Damage Buff", true));
            ActivatorMenu.Add(new MenuSlider("AutoUsePotsHealth", "Use if Health Percent is below or equal", 1, 100, 30));

            /* Combo Section */
            ComboMenu = HomeMenu.AddSubMenu("> COMBO");
            ComboMenu.AddSeparator("-Combo Settings-");

            /* LaneClear Section */
            LaneClearMenu = HomeMenu.AddSubMenu("> LANE CLEAR");
            LaneClearMenu.AddSeparator("-Mana Options-");
            LaneClearMenu.Add(new MenuSlider("MaxMana", "Min Mana Percent to use Spells", 0, 100, 45));

            /* LastHit Section */
            LastHitMenu = HomeMenu.AddSubMenu("> LAST HIT");
            LastHitMenu.AddSeparator("-Mana Options-");
            LastHitMenu.Add(new MenuSlider("MaxMana", "Min Mana % to use Spells", 0, 100, 45));

            /* Visuals Section */
            VisualsMenu = HomeMenu.AddSubMenu("> VISUALS");
            VisualsMenu.AddSeparator("-Drawings-");
            VisualsMenu.Add(new MenuCheckbox("DrawSpellsRange", "Draw Spells Range", true));
            VisualsMenu.Add(new MenuCheckbox("DrawDamage", "Draw Damage Indicator", true));
            VisualsMenu.Add(new MenuCheckbox("DrawBoundingRadius", "Draw Enemy Champion Hit Radius", true));
            VisualsMenu.AddSeparator("-Spell Drawing Options-");
            VisualsMenu.Add(new MenuCheckbox("DrawQ", "Draw Q", true));
            VisualsMenu.Add(new MenuCheckbox("DrawE", "Draw E", true));
            VisualsMenu.Add(new MenuCheckbox("DrawR", "Draw R", true));
            VisualsMenu.AddSeparator("-Extra Options-");
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyQ", "Draw Q only when ready", true));
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyE", "Draw E only when ready", true));
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyR", "Draw R only when ready", true));

            /* Misc Section */
            MiscMenu = HomeMenu.AddSubMenu("> MISC");
            MiscMenu.AddSeparator("-Killsteal-");
            MiscMenu.Add(new MenuCheckbox("KillSteal", "Auto KillSteal"));
            MiscMenu.AddSeparator("-Passive-");
            MiscMenu.Add(new MenuCheckbox("AutoStackPassive", "Auto Stack Passive", true));
            MiscMenu.Add(new MenuSlider("StackPassiveManaSpawn", "Min Mana % to use W/E in Spawn", 0, 100, 90));
            MiscMenu.Add(new MenuSlider("StackPassiveMana", "Min Mana % to Auto Stack with E", 0, 100, 90));
            /* MiscMenu.AddSeparator("-Interrupt-");
            MiscMenu.Add(new MenuCheckbox("InterruptOnGapCloser", "Interrupt Enemy GapCloser with Stun", true));
            MiscMenu.Add(new MenuCheckbox("InterruptPassive", "Use Passive to Interrupt Enemy"));
            MiscMenu.Add(new MenuCheckbox("InterruptR", "Use R to Interrupt Enemy")); TODO: IMPLEMENT */
            MiscMenu.AddSeparator("-Auto Leveler-");
            MiscMenu.Add(new MenuCheckbox("AutoLevel", "Enable Auto Leveler"));
            MiscMenu.Add(new MenuCombo("AutoLevelFirstFocus", "1 Spell to Focus", new[] { "Q", "W", "E" }));
            MiscMenu.Add(new MenuCombo("AutoLevelSecondFocus", "2 Spell to Focus", new[] { "Q", "W", "E" }));
            MiscMenu.Add(new MenuCombo("AutoLevelThirdFocus", "3 Spell to Focus", new[] { "Q", "W", "E" }));
            MiscMenu.Add(new MenuSlider("AutoLevelDelaySlider", "Delay Slider", 200, 150, 500));

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
