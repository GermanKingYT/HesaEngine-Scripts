﻿using System;

using HesaEngine.SDK;

namespace _HESA_T2IN1_REBORN_ANNIE.Visuals
{
    internal class Menus
    {
        public static Menu Home;
        public static Menu ActivatorMenu;
        public static Menu ComboMenu;
        public static Menu LaneClearMenu;
        public static Menu LastHitMenu;
        public static Menu VisualsMenu;
        public static Menu MiscMenu;

        public static void Initialize()
        {
            Home = Menu.AddMenu("[T2IN1-REBORN] " + Globals.MyHero.ChampionName);

            Globals.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("> ORBWALKER"));

            /* Activator Section */
            ActivatorMenu = Home.AddSubMenu("> ACTIVATOR");
            ActivatorMenu.Add(new MenuCheckbox("AutoUsePots", "Auto use Health Pots", true));
            ActivatorMenu.Add(new MenuCheckbox("AutoUsePotsOnBuff", "Auto use Health Pots if Poisened / has Damage Buff", true));
            ActivatorMenu.Add(new MenuSlider("AutoUsePotsHealth", "Use if Health Percent is below or equal", 1, 100, 30));

            /* Combo Section */
            ComboMenu = Home.AddSubMenu("> COMBO");
            ComboMenu.Add(new MenuCheckbox("UseQ", "Use Q in Combo", true));
            ComboMenu.Add(new MenuCheckbox("UseW", "Use W in Combo", true));
            ComboMenu.Add(new MenuCheckbox("UseE", "Use E in Combo (if Stun is not ready)", true));
            ComboMenu.Add(new MenuCheckbox("UseR", "Use R in Combo", true));
            ComboMenu.Add(new MenuCheckbox("OnlyIfStunReadyR", "Use R only if Stun is ready"));
            ComboMenu.Add(new MenuSlider("UltimateTargets", "Min Enemys Hitable to use R", 1, 5, 1));
            ComboMenu.AddSeparator("-Tibbers Options-");
            var _ControlMethodR = new[] { "Lowest HP", "Nearest", "Selected" };
            ComboMenu.Add(new MenuCheckbox("AutoControl", "Auto Control", true));
            ComboMenu.Add(new MenuCombo("ControlMethodTibbers", "Auto Control Target", _ControlMethodR));
            ComboMenu.Add(new MenuCheckbox("DontAttackIfUnderTurret", "Dont Attack if Target is under Turret", true));

            /* LaneClear Section */
            LaneClearMenu = Home.AddSubMenu("> LANE CLEAR");
            LaneClearMenu.AddSeparator("-Spells-");
            LaneClearMenu.Add(new MenuCheckbox("UseQ", "Use Q", true));
            LaneClearMenu.Add(new MenuCheckbox("UseOnlyIfKillable", "Use Q only if Killable", true));
            LaneClearMenu.Add(new MenuCheckbox("UseW", "Use W", true));
            LaneClearMenu.AddSeparator("-Extra Options-");
            LaneClearMenu.Add(new MenuCheckbox("StopIfPassiveIsCharged", "Dont use Spells if Passive is Charged"));
            LaneClearMenu.Add(new MenuSlider("MinMinions", "Min Minions Hitable to use W", 1, 5, 3));
            LaneClearMenu.AddSeparator("-Mana Options-");
            LaneClearMenu.Add(new MenuSlider("MaxMana", "Min Mana % to use Spells", 0, 100, 45));

            /* LastHit Section */
            LastHitMenu = Home.AddSubMenu("> LAST HIT");
            LastHitMenu.AddSeparator("-Spells-");
            LastHitMenu.Add(new MenuCheckbox("UseQ", "Use Q", true));
            LastHitMenu.Add(new MenuCheckbox("UseW", "Use W", true));
            LastHitMenu.AddSeparator("-Extra Options-");
            /* LastHit.Add(new MenuSlider("MinMinions", "Minimun Minions Killable to use W", 1, 5, 3)); TODO: MAYBE IMPLEMENT */
            LastHitMenu.Add(new MenuCheckbox("StopIfPassiveIsCharged", "Dont use Spells if Passive is charged"));
            LastHitMenu.AddSeparator("-Mana Options-");
            LastHitMenu.Add(new MenuSlider("MaxMana", "Min Mana % to use Spells", 0, 100, 45));

            /* Visuals Section */
            VisualsMenu = Home.AddSubMenu("> VISUALS");
            VisualsMenu.AddSeparator("-Drawings-");
            VisualsMenu.Add(new MenuCheckbox("DrawSpellsRange", "Draw Spells Range", true));
            VisualsMenu.Add(new MenuCheckbox("DrawDamage", "Draw Damage Indicator", true));
            VisualsMenu.Add(new MenuCheckbox("DrawBoundingRadius", "Draw Champion Hit Radius", true));
            VisualsMenu.Add(new MenuCheckbox("DrawPredictionR", "Draw R Prediction", true));
            VisualsMenu.Add(new MenuCheckbox("DrawPredictionW", "Draw W Prediction", true));
            VisualsMenu.AddSeparator("-Spell Drawing Options-");
            VisualsMenu.Add(new MenuCheckbox("DrawQ", "Draw Q", true));
            VisualsMenu.Add(new MenuCheckbox("DrawW", "Draw W", true));
            VisualsMenu.Add(new MenuCheckbox("DrawR", "Draw R", true));
            VisualsMenu.AddSeparator("-Extra Options-");
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyQ", "Draw Q only when ready", true));
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyW", "Draw W only when ready", true));
            VisualsMenu.Add(new MenuCheckbox("DrawOnlyWhenReadyR", "Draw R only when ready", true));

            /* Misc Section */
            MiscMenu = Home.AddSubMenu("> MISC");
            MiscMenu.AddSeparator("-Summoners-");
            MiscMenu.Add(new MenuCheckbox("AutoIgnite", "Auto use Ignite if Killable"));
            MiscMenu.AddSeparator("-Killsteal-");
            MiscMenu.Add(new MenuCheckbox("KillSteal", "Auto KillSteal"));
            MiscMenu.AddSeparator("-Passive-");
            MiscMenu.Add(new MenuCheckbox("AutoStackPassive", "Auto Stack Passive", true));
            MiscMenu.Add(new MenuSlider("StackPassiveManaSpawn", "Min Mana % to use W/E in Spawn", 0, 100, 90));
            MiscMenu.Add(new MenuSlider("StackPassiveMana", "Min Mana % to Auto Stack with E", 0, 100, 90));
            MiscMenu.AddSeparator("-Interrupt-");
            MiscMenu.Add(new MenuCheckbox("InterruptOnGapCloser", "Interrupt Enemy GapCloser with Stun", true));
            /* MiscMenu.Add(new MenuCheckbox("InterruptPassive", "Use Passive to Interrupt Enemy"));
            MiscMenu.Add(new MenuCheckbox("InterruptR", "Use R to Interrupt Enemy")); TODO: IMPLEMENT */
            /* Misc.AddSeparator("-Auto Leveler-"); 
            Misc.Add(new MenuCheckbox("AutoLevel", "Enable Auto Leveler"));
            Misc.Add(new MenuCombo("AutoLevelFirstFocus", "1 Spell to Focus", new[] { "Q", "W", "E" }));
            Misc.Add(new MenuCombo("AutoLevelSecondFocus", "2 Spell to Focus", new[] { "Q", "W", "E" }));
            Misc.Add(new MenuCombo("AutoLevelThirdFocus", "3 Spell to Focus", new[] { "Q", "W", "E" }));
            Misc.Add(new MenuSlider("AutoLevelDelaySlider", "Delay Slider", 200, 150, 500)); TODO: FINISH */

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}