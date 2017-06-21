using System;
using System.Linq;

using T2IN1_REBORN_LIB.Helpers;

using HesaEngine.SDK;
using SharpDX;

namespace T2IN1_REBORN_LIB.Features
{
    public static class Drawings
    {
        public struct Draw
        {
            public struct Q
            {
                public static bool Enabled = false;
                public static Color Color = Color.White;
                public static bool OnlyWhenUsable = false;
            }

            public struct W
            {
                public static bool Enabled = false;
                public static Color Color = Color.White;
                public static bool OnlyWhenUsable = false;
            }

            public struct E
            {
                public static bool Enabled = false;
                public static Color Color = Color.White;
                public static bool OnlyWhenUsable = false;
            }

            public struct R
            {
                public static bool Enabled = false;
                public static Color Color = Color.White;
                public static bool OnlyWhenUsable = false;
            }

            public struct BoundingRadius
            {
                public struct Enemy
                {
                    public static bool Enabled = false;
                    public static Color Color = Color.White;
                }

                public struct Ally
                {
                    public static bool Enabled = false;
                    public static Color Color = Color.White;
                }
            }

            public struct DamageIndicator
            {
                public static bool Enabled = false;
            }
        }

        public static void Initialize()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (ObjectManager.Me.IsDead)
                return;

            if (Draw.Q.Enabled && Draw.Q.OnlyWhenUsable ? Spells.Q.IsUsable() : Draw.Q.Enabled)
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, Spells.Q.Range, Draw.Q.Color, 1);
            }

            if (Draw.Q.Enabled && Draw.Q.OnlyWhenUsable ? Spells.W.IsUsable() : Draw.Q.Enabled) 
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, Spells.W.Range, Draw.Q.Color, 1);
            }

            if (Draw.Q.Enabled && Draw.Q.OnlyWhenUsable ? Spells.E.IsUsable() : Draw.Q.Enabled) 
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, Spells.E.Range, Draw.Q.Color, 1);
            }

            if (Draw.Q.Enabled && Draw.Q.OnlyWhenUsable ? Spells.R.IsUsable() : Draw.Q.Enabled) 
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, Spells.R.Range, Draw.Q.Color, 1);
            }

            if (Draw.BoundingRadius.Ally.Enabled) 
            {
                foreach (var ally in Entities.CachedAllies.Where(x => x.IsVisibleOnScreen)) 
                {
                    Drawing.DrawCircle(ally.Position, ally.BoundingRadius, Draw.BoundingRadius.Ally.Color);
                }
            }

            if (Draw.BoundingRadius.Enemy.Enabled)
            {
                foreach (var enemy in Entities.CachedEnemies.Where(x => x.IsVisibleOnScreen && x.IsVisible)) 
                {
                    Drawing.DrawCircle(enemy.Position, enemy.BoundingRadius, Draw.BoundingRadius.Enemy.Color);
                }
            }

            if (Draw.DamageIndicator.Enabled)
            {
                foreach (var enemy in Entities.CachedEnemies.Where(x => x.IsVisibleOnScreen && x.IsVisible))
                {
                    double damage = Spells.TotalSpellDamage(enemy);

                    Vector2 screenPosition = Drawing.WorldToScreen(enemy.Position);
                    Vector2 textPosition = new Vector2(screenPosition.X, screenPosition.Y);
                    Color color = new ColorBGRA(152, 219, 52, 255);
                    double estimatedDamage = Math.Ceiling((int)damage / enemy.Health * 100);
                    string text = "Estimated Damage: NaN";


                    if (damage > 0)
                    {
                        if (estimatedDamage > 100) 
                        {
                            text = "Is Killable";
                            color = Color.Red;
                        }
                        else 
                        {
                            text = "Estimated Damage: " + string.Concat(estimatedDamage, "%");
                        }
                    }

                    Drawing.DrawText(textPosition, color, text);
                }
            }
        }
    }
}
