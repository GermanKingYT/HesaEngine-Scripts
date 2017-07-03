using System;

using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_AIO.Champions
{
    internal class Ahri
    {
        internal class Events
        {
            public static void Load()
            {
                Drawing.OnDraw += Drawing_OnDraw;
                GameObject.OnCreate += GameObject_OnCreate;
                GameObject.OnDelete += GameObject_OnDelete;
            }

            private static void GameObject_OnCreate(GameObject sender, EventArgs args)
            {
                if (sender.IsEnemy || sender.ObjectType != GameObjectType.Missile  || !sender.IsValid<MissileClient>()) return;

                Logger.Log("[OnCreate] Missile Object is Valid");

                MissileClient missile = (MissileClient)sender;
                {
                    if (missile.SData.Name != null && string.Equals(missile.SData.Name, "AhriOrbMissile", StringComparison.CurrentCultureIgnoreCase) || string.Equals(missile.SData.Name, "AhriOrbReturn", StringComparison.CurrentCultureIgnoreCase)) MissileObject = missile;
                }
            }

            private static void GameObject_OnDelete(GameObject sender, EventArgs args)
            {
                if (sender.IsEnemy || sender.ObjectType != GameObjectType.Missile || !sender.IsValid<MissileClient>()) return;

                Logger.Log("[OnDelete] Missile Object is Valid");

                MissileClient missile = (MissileClient)sender;
                {
                    if (missile.SData.Name != null && string.Equals(missile.SData.Name, "AhriOrbReturn", StringComparison.CurrentCultureIgnoreCase)) MissileObject = null;
                }
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

                if (MissileObject != null && MissileObject.IsValid() && VisualsMenu.GetCheckbox("drawMissile")) Library.Extensions.DrawLineRectangle(ObjectManager.Me.Position.To2D(), MissileObject.Position.To2D(), (int)Q.Width, 1, Color.White);
            }
        }

        private static Menu VisualsMenu;
        private static Spell Q, W, E, R;
        private static MissileClient MissileObject;

        public static void Initialize()
        {
            Q = new Spell(SpellSlot.Q, 870);
            W = new Spell(SpellSlot.W, 580);
            E = new Spell(SpellSlot.E, 950);
            R = new Spell(SpellSlot.R, 600);

            Q.SetSkillshot(0.25f, 90, 1550, false, SkillshotType.SkillshotLine);
            E.SetSkillshot(0.25f, 60, 1550, true, SkillshotType.SkillshotLine);

            Globals.ChampionMenu = Globals.RootMenu.AddSubMenu(ObjectManager.Me.Hero.ToString().ToUpper());

            VisualsMenu = Globals.ChampionMenu.AddSubMenu("VISUALS");
            VisualsMenu.AddSeparator("-SPELLS-");
            VisualsMenu.AddMenuCheckbox("drawWhenRdy", "Draw Spells only when ready", true);
            VisualsMenu.AddMenuCheckbox("drawQ", "Draw Q", true);
            VisualsMenu.AddMenuCheckbox("drawW", "Draw W", true);
            VisualsMenu.AddMenuCheckbox("drawE", "Draw E", true);
            VisualsMenu.AddMenuCheckbox("drawR", "Draw R", true);
            VisualsMenu.AddSeparator("-EXTRA-");
            VisualsMenu.AddMenuCheckbox("drawMissile", "Draw Q Hitbox", true);

            Events.Load();

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Ahri is loaded, have fun", "#27ae60");
        }
    }
}