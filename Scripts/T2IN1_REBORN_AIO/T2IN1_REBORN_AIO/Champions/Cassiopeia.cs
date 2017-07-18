using System;

using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;

namespace T2IN1_REBORN_AIO.Champions
{
    internal class Cassiopeia
    {
        private static void InitializeEvents()
        {
            Drawing.OnDraw += Drawing_OnDraw;
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

        private static Menu VisualsMenu;
        private static Spell Q, W, E, R;

        public static void Initialize()
        {
            Q = new Spell(SpellSlot.Q, 850, TargetSelector.DamageType.Magical);
            W = new Spell(SpellSlot.W, 800, TargetSelector.DamageType.Magical);
            E = new Spell(SpellSlot.E, 700, TargetSelector.DamageType.Magical);
            R = new Spell(SpellSlot.R, 825, TargetSelector.DamageType.Magical);

            Q.SetSkillshot(0.3f, 50f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            W.SetSkillshot(0.5f, 125f, 2500f, false, SkillshotType.SkillshotCircle);
            E.SetTargetted(0.2f, 1700f);
            R.SetSkillshot(0.8f, (float)(80 * Math.PI / 180), float.MaxValue, false, SkillshotType.SkillshotCone);

            Globals.ChampionMenu = Globals.RootMenu.AddSubMenu(ObjectManager.Me.Hero.ToString().ToUpper());

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