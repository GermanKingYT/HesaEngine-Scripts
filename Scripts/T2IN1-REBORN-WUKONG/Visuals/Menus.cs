using System;

using HesaEngine.SDK;

namespace T2IN1_REBORN_WUKONG.Visuals
{
    internal class Menus
    {
        public static Menu HomeMenu, ActivatorMenu, ComboMenu, JungleClearMenu, LaneClearMenu, LastHitMenu, VisualsMenu, MiscMenu;

        public static void Initialize()
        {
            HomeMenu = Menu.AddMenu("[T2IN1-REBORN] " + Globals.MyHero.ChampionName);

            /* Combo Section */
            ComboMenu = HomeMenu.AddSubMenu("> COMBO");
            ComboMenu.AddSeparator("-Combo Settings-");
            ComboMenu.Add(new MenuCheckbox("UseQ", "Use Q in Combo", true));
            ComboMenu.Add(new MenuCheckbox("UseE", "Use E in Combo", true));
            ComboMenu.Add(new MenuCheckbox("UseR", "Use R in Combo", true));
            ComboMenu.AddSeparator("-Extra Options-");
            ComboMenu.Add(new MenuCheckbox("UseTiamat", "Use Tiamat for AA Cancel", true));
            ComboMenu.Add(new MenuCheckbox("UseHydra", "Use Hydra for AA Cancel", true));
            ComboMenu.Add(new MenuSlider("MinEnemiesHitableR", "Min Enemys Hitable to use R", 1, 5, 1));

            /* JungleClear Section */
            JungleClearMenu = HomeMenu.AddSubMenu("> JUNGLE CLEAR");
            JungleClearMenu.AddSeparator("-Spells-");
            JungleClearMenu.Add(new MenuCheckbox("UseQ", "Use Q", true));
            JungleClearMenu.Add(new MenuCheckbox("UseW", "Use W", true));
            JungleClearMenu.Add(new MenuCheckbox("UseE", "Use E", true));
            JungleClearMenu.AddSeparator("-Mana Options-");
            JungleClearMenu.Add(new MenuCheckbox("UseTiamat", "Use Tiamat for AA Cancel", false));
            JungleClearMenu.Add(new MenuCheckbox("UseHydra", "Use Hydra for AA Cancel", false));
            JungleClearMenu.Add(new MenuSlider("MaxMana", "Min Mana Percent to use Spells", 0, 100, 45));

            /* LaneClear Section */
            LaneClearMenu = HomeMenu.AddSubMenu("> LANE CLEAR");
            LaneClearMenu.AddSeparator("-Spells-");
            LaneClearMenu.Add(new MenuCheckbox("UseQ", "Use Q", true));
            LaneClearMenu.Add(new MenuCheckbox("UseW", "Use W", true));
            LaneClearMenu.Add(new MenuCheckbox("UseE", "Use E", true));
            LaneClearMenu.AddSeparator("-Extra Options-");
            LaneClearMenu.Add(new MenuSlider("MinMinionsW", "Min Hitable Minions to use W", 1, 5, 1));
            LaneClearMenu.Add(new MenuSlider("MinMinionsE", "Min Hitable Minions to use E", 1, 5, 1));
            LaneClearMenu.Add(new MenuCheckbox("UseTiamat", "Use Tiamat for AA Cancel", false));
            LaneClearMenu.Add(new MenuCheckbox("UseHydra", "Use Hydra for AA Cancel", false));
            LaneClearMenu.AddSeparator("-Mana Options-");
            LaneClearMenu.Add(new MenuSlider("MaxMana", "Min Mana Percent to use Spells", 0, 100, 45));

            /* LastHit Section */
            LastHitMenu = HomeMenu.AddSubMenu("> LAST HIT");
            LastHitMenu.AddSeparator("-Spells-");
            LastHitMenu.Add(new MenuCheckbox("UseQ", "Use Q", true));
            LastHitMenu.Add(new MenuCheckbox("UseW", "Use W", true));
            LastHitMenu.Add(new MenuCheckbox("UseE", "Use E", true));
            LastHitMenu.AddSeparator("-Extra Options-");
            LastHitMenu.Add(new MenuCheckbox("UseTiamat", "Use Tiamat for AA Cancel", false));
            LastHitMenu.Add(new MenuCheckbox("UseHydra", "Use Hydra for AA Cancel", false));
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
            /* MiscMenu.AddSeparator("-Interrupt-");
            MiscMenu.Add(new MenuCheckbox("InterruptOnGapCloser", "Interrupt Enemy GapCloser with Stun", true));
            MiscMenu.Add(new MenuCheckbox("InterruptPassive", "Use Passive to Interrupt Enemy"));
            MiscMenu.Add(new MenuCheckbox("InterruptR", "Use R to Interrupt Enemy")); TODO: IMPLEMENT */
            MiscMenu.AddSeparator("-Auto Leveler-");
            MiscMenu.Add(new MenuCheckbox("AutoLevel", "Enable Auto Leveler"));
            MiscMenu.Add(new MenuCombo("AutoLevelFirstFocus", "1 Spell to Focus", new[] { "Q", "W", "E" }, 0));
            MiscMenu.Add(new MenuCombo("AutoLevelSecondFocus", "2 Spell to Focus", new[] { "Q", "W", "E" }, 1));
            MiscMenu.Add(new MenuCombo("AutoLevelThirdFocus", "3 Spell to Focus", new[] { "Q", "W", "E" }, 2));
            MiscMenu.Add(new MenuSlider("AutoLevelDelaySlider", "Delay Slider", 200, 150, 500));

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
