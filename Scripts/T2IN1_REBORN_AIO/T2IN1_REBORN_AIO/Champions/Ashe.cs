using System;
using System.Linq;

using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_AIO.Champions
{
    internal class Ashe
    {
        private static void InitializeEvents()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (VisualsMenu.GetCheckbox("drawQ")) 
            {
                if (VisualsMenu.GetCheckbox("drawWhenRdy")) 
                {
                    if (Q.IsReady()) Drawing.DrawCircle(ObjectManager.Me.Position, Q.Range, Color.Orange, 1);
                }
                else 
                {
                    Drawing.DrawCircle(ObjectManager.Me.Position, Q.Range, Color.Aqua, 1);
                }
            }

            if (VisualsMenu.GetCheckbox("drawW")) 
            {
                if (VisualsMenu.GetCheckbox("drawWhenRdy")) 
                {
                    if (W.IsReady()) Drawing.DrawCircle(ObjectManager.Me.Position, W.Range, Color.Gray, 1);
                }
                else 
                {
                    Drawing.DrawCircle(ObjectManager.Me.Position, W.Range, Color.Gray, 1);
                }
            }

            if (VisualsMenu.GetCheckbox("drawE")) 
            {
                if (VisualsMenu.GetCheckbox("drawWhenRdy")) 
                {
                    if (E.IsReady()) Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Aqua, 1);
                }
                else 
                {
                    Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Aqua, 1);
                }
            }

            if (VisualsMenu.GetCheckbox("drawR")) 
            {
                if (VisualsMenu.GetCheckbox("drawWhenRdy")) 
                {
                    if (R.IsReady()) Drawing.DrawCircle(ObjectManager.Me.Position, R.Range, Color.Violet, 1);
                }
                else 
                {
                    Drawing.DrawCircle(ObjectManager.Me.Position, R.Range, Color.Violet, 1);
                }
            }
        }

        private static void Game_OnUpdate()
        {
            if (!ObjectManager.Me.IsDead || !ObjectManager.Me.CanCast) return;

            switch (Globals.OrbMode) 
            {
                case Orbwalker.OrbwalkingMode.Combo:
                    DoCombo();
                    return;
            }
        }

        private static void DoCombo()
        {
            AIHeroClient target = Cache.Enemies.Where(x => x.IsValidEntity()).MinOrDefault(x => x.Health);
            if (target == null) return;

            if (ComboMenu.GetCheckbox("useQ") && target.IsValidEntity() && Q.IsReady()) Q.Cast();

            if (ComboMenu.GetCheckbox("useW") && target.IsValidEntity() && W.IsReady()) W.CastPrediction(target, PredictionMenu.GetCombobox("predictionW"));

            if (ComboMenu.GetCheckbox("useE") && target.IsValidEntity() && E.IsReady()) { }
            if (ComboMenu.GetCheckbox("useR") && target.IsValidEntity() && R.IsReady()) { }
        }

        private static Menu ComboMenu, VisualsMenu, PredictionMenu;
        private static Spell Q, W, E, R;

        public static void Initialize()
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W, 1200f);
            E = new Spell(SpellSlot.E, float.MaxValue);
            R = new Spell(SpellSlot.R, float.MaxValue);

            W.SetSkillshot(0.25f, 20f, 1200f, true, SkillshotType.SkillshotLine);
            E.SetSkillshot(0.25f, 299f, 1400f, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.SkillshotLine);

            Globals.ChampionMenu = Globals.RootMenu.AddSubMenu(ObjectManager.Me.Hero.ToString().ToUpper());

            PredictionMenu = Globals.ChampionMenu.AddSubMenu("PREDICTION");
            PredictionMenu.AddMenuCombobox("predictionW", "Prediction W", new[] { "Very High", "High", "Medium" }, 0);
            PredictionMenu.AddMenuCombobox("predictionR", "Prediction R", new[] { "Very High", "High", "Medium" }, 0);

            ComboMenu = Globals.ChampionMenu.AddSubMenu("COMBO");
            ComboMenu.AddSeparator("-SPELLS-");
            ComboMenu.AddMenuCheckbox("useQ", "Use Q", true);
            ComboMenu.AddMenuCheckbox("useW", "Use W", true);
            ComboMenu.AddMenuCheckbox("useE", "Use E", true);
            ComboMenu.AddMenuCheckbox("useR", "Use R", true);

            VisualsMenu = Globals.ChampionMenu.AddSubMenu("VISUALS");
            VisualsMenu.AddSeparator("-SPELLS-");
            VisualsMenu.AddMenuCheckbox("drawWhenRdy", "Draw Spells only when ready", true);
            VisualsMenu.AddMenuCheckbox("drawQ", "Draw Q", true);
            VisualsMenu.AddMenuCheckbox("drawW", "Draw W", true);
            VisualsMenu.AddMenuCheckbox("drawE", "Draw E", true);
            VisualsMenu.AddMenuCheckbox("drawR", "Draw R", true);

            InitializeEvents();

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Ahri is loaded, have fun", "#27ae60");
        }
    }
}
